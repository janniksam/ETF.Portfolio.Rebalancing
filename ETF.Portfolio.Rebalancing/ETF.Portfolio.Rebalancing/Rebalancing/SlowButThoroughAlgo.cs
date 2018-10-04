using ETF.Portfolio.Rebalancing.CalculationModel;
using ETF.Portfolio.Rebalancing.Data;
using ETF.Portfolio.Rebalancing.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETF.Portfolio.Rebalancing.Rebalancing
{
    /// <summary>
    /// This algo calculates the optimal outcome from each possible combination.
    /// 
    /// --- This algo follows 1 constraint: ---
    /// The total order value of he ETF with the worst variance has to be bigger than the value of EW-1.
    /// EW0 > EW-1 > EW-2 > ... > EW-N
    /// ---------------------------------------
    /// 
    /// This algo's performance is ok'ish for small amounts of money (smaller 1500), 
    /// but it will perform terrible with bigger investment amount.
    /// The result on the other site is very precise.
    /// </summary>
    public class SlowButThoroughAlgo : IRebalancer
    { 
        private RebalanceInput m_input;
        private int m_maxDepth;
        private RebalanceResult m_result;

        public SlowButThoroughAlgo()
        {
        }

        public RebalanceResult Rebalance(RebalanceInput input)
        {
            Init(input);

            var itemsToBuy = CalculateItemsToBuy();
            var dict = m_input.Portfolio.ToDictionary(p => p.ISIN);

            CalculateRecursive(dict, itemsToBuy, m_input.MoneyToSpent, m_input.MoneyToSpent, 0);
            return m_result;
        }

        private void Init(RebalanceInput input)
        {
            m_input = input ?? throw new ArgumentNullException(nameof(input));
            if (m_input.MaxOrderCount < 1 || m_input.MaxOrderCount > input.Portfolio.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(input.MaxOrderCount));
            }

            m_maxDepth = m_input.MaxOrderCount - 1;
            m_result = new RebalanceResult();
        }

        private List<Order> CalculateItemsToBuy()
        {
            CalculateCurrentAllocation(m_input.Portfolio);
            return m_input.Portfolio
                .OrderBy(p => p.Variance)
                .Take(m_input.MaxOrderCount)
                .Select(p => new Order { CurrentPrice = p.CurrentPrice, ISIN = p.ISIN })
                .ToList();
        }

        private void CalculateRecursive(
            Dictionary<string, ETFItem> portfolio,
            List<Order> itemsToBuy,
            decimal moneyLeftToSpent,
            decimal maxMoneyToSpentOnOrder,
            int depth = 0)
        {
            var item = itemsToBuy[depth];
            if (m_maxDepth == depth)
            {
                short canBuyAmountLast = (short)(moneyLeftToSpent / item.CurrentPrice);
                item.OrderAmount = canBuyAmountLast;

                foreach (var order in itemsToBuy)
                {
                    portfolio[order.ISIN].CurrentlyOwned += order.OrderAmount;
                }

                decimal portfolioAllocationThen = CalculateCurrentAllocation(portfolio.Select(p => p.Value));

                lock (m_result)
                {
                    if (m_result.Percentage > portfolioAllocationThen)
                    {
                        m_result.Percentage = portfolioAllocationThen;
                        m_result.Orders = itemsToBuy.Clone();
                    }
                }

                foreach (var order in itemsToBuy)
                {
                    portfolio[order.ISIN].CurrentlyOwned -= order.OrderAmount;
                }

                return;
            }

            short canBuyAmount = (short)(maxMoneyToSpentOnOrder / item.CurrentPrice);
            if (depth == 0)
            {
                var maxDegreeOfParallelism = Environment.ProcessorCount / 2 < 2 ? 2 : Environment.ProcessorCount / 2;
                Parallel.For(0, canBuyAmount + 1, new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism }, increment =>
                {
                    var clonedPortfolio = portfolio.Clone();
                    var clonedOrder = (List<Order>)itemsToBuy.Clone();

                    var currentItem = clonedOrder.First(p => p.ISIN == item.ISIN);
                    currentItem.OrderAmount = (short)increment;

                    var maxMoneyToSpentOnNextOrder = increment * currentItem.CurrentPrice;
                    var moneyLeftToSpentAfterOrder = moneyLeftToSpent - maxMoneyToSpentOnNextOrder;
                    if (maxMoneyToSpentOnNextOrder == 0)
                    {
                        maxMoneyToSpentOnNextOrder = maxMoneyToSpentOnOrder;
                    }
                    else if (maxMoneyToSpentOnNextOrder > moneyLeftToSpentAfterOrder)
                    {
                        maxMoneyToSpentOnNextOrder = moneyLeftToSpentAfterOrder;
                    }

                    CalculateRecursive(clonedPortfolio, clonedOrder, moneyLeftToSpentAfterOrder, maxMoneyToSpentOnNextOrder, depth + 1);
                });
            }
            else
            {
                for (short i = canBuyAmount; i >= 0; i--)
                {
                    item.OrderAmount = i;

                    var maxMoneyToSpentOnNextOrder = i * item.CurrentPrice;
                    var moneyLeftToSpentAfterOrder = moneyLeftToSpent - maxMoneyToSpentOnNextOrder;
                    if (maxMoneyToSpentOnNextOrder == 0)
                    {
                        maxMoneyToSpentOnNextOrder = maxMoneyToSpentOnOrder;
                    }
                    else if (maxMoneyToSpentOnNextOrder > moneyLeftToSpentAfterOrder)
                    {
                        maxMoneyToSpentOnNextOrder = moneyLeftToSpentAfterOrder;
                    }

                    CalculateRecursive(portfolio, itemsToBuy, moneyLeftToSpentAfterOrder, maxMoneyToSpentOnNextOrder, depth + 1);
                }
            }
        }

        private decimal CalculateCurrentAllocation(IEnumerable<ETFItem> items)
        {
            var totalPortfolioValue = items.Sum(p => p.CurrentPrice * p.CurrentlyOwned);
           
            foreach (var item in items)
            {
                item.ActualPercentage = totalPortfolioValue == 0 
                    ? 0
                    : (item.CurrentPrice * item.CurrentlyOwned) / totalPortfolioValue;
            }

            return items.Sum(p => Math.Abs(p.Variance));
        }
    }
}
