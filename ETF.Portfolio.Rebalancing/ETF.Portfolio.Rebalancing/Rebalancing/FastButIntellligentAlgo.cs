using ETF.Portfolio.Rebalancing.Data;
using ETF.Portfolio.Rebalancing.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETF.Portfolio.Rebalancing.Rebalancing
{
    /// <summary>
    /// This algorithm simulates to order piece after piece and recalculates the variance after each
    /// order. It is pretty fast but also very efficient and clever.
    /// </summary>
    public class FastButIntelligentAlgo : IRebalancer
    {
        public RebalanceResult Rebalance(RebalanceInput input)
        {
            var bestPercentage = decimal.MaxValue;
            List<ETFItem> bestPortfolio = null;

            // When the max order amount is minimized, we get all possible item 
            // combinations to try out
            var combinations = input.Portfolio.GetKCombs(input.MaxOrderCount);

            var mutex = new object();

            // for each possible combination...
            combinations.AsParallel().ForAll(c =>
            {
                // Simulating investment with this combination
                var newPortfolio = SimulateInvestment(input, c);

                // Calculating final percentage for this combination
                var newPercentage = CalculateCurrentAllocation(newPortfolio);
                lock (mutex)
                {
                    // if combination ends up better than the previous combinations, remember it
                    if (newPercentage < bestPercentage)
                    {
                        bestPercentage = newPercentage;
                        bestPortfolio = newPortfolio;
                    }
                }
            });
       
            // Calculation is done, creating the orders and returning the final result
            List<Order> order = new List<Order>();
            foreach (var item in input.Portfolio)
            {
                var itemAfter = bestPortfolio.First(p => p.ISIN == item.ISIN);
                if (itemAfter.CurrentlyOwned > item.CurrentlyOwned)
                {
                    order.Add(new Order { ISIN = itemAfter.ISIN, CurrentPrice = itemAfter.CurrentPrice, OrderAmount = (short)(itemAfter.CurrentlyOwned - item.CurrentlyOwned) });
                }
            }

            var result = new RebalanceResult();
            result.Percentage = bestPercentage;
            result.Orders = order;

            return result;
        }

        private List<ETFItem> SimulateInvestment(RebalanceInput input, IEnumerable<ETFItem> itemsToBuy)
        {
            var clonedItemsToBuy = itemsToBuy.OrderBy(p => p.Variance).ToList().Clone();
            var clonedPortfolio = input.Portfolio.Clone();
            var moneyLeft = input.MoneyToSpent;

            // Investing money until nothing else can be bought anymore
            while (clonedItemsToBuy.Any(p => p.CurrentPrice <= moneyLeft))
            {
                ETFItem bestETF = null;
                decimal bestPercentage = decimal.MaxValue;
                // foreach item in the combination... 
                foreach (var itemToBuy in clonedItemsToBuy)
                {
                    if (itemToBuy.CurrentPrice > moneyLeft)
                    {
                        continue;
                    }

                    // calculate "before" percentage
                    var beforePercentage = CalculateCurrentAllocation(clonedPortfolio);

                    var etf = clonedPortfolio.First(p => p.ISIN == itemToBuy.ISIN);
                    etf.CurrentlyOwned++;

                    // calculate "after" percentage
                    decimal percentage = CalculateCurrentAllocation(clonedPortfolio);

                    // here comes the "magic"
                    if (percentage < beforePercentage && etf.Variance < 0)
                    {
                        // this is the best investment for now, lets get out of here right now
                        bestETF = etf;
                        bestPercentage = percentage;
                        etf.CurrentlyOwned--;
                        break;
                    }
                    else if (bestETF == null || percentage < bestPercentage)
                    {
                        // this could be the best investment, but let's see if other investments are even better
                        bestETF = etf;
                        bestPercentage = percentage;
                        etf.CurrentlyOwned--;
                    }
                    else
                    {
                        // this is a bad investment, do a rollback and forget about it
                        etf.CurrentlyOwned--;
                    }
                }
                
                bestETF.CurrentlyOwned++;
                moneyLeft -= bestETF.CurrentPrice;
            }
            
            // the optimal future portfolio
            return clonedPortfolio.ToList();
        }

        private List<ETFItem> CalculateItemsToBuy(RebalanceInput input)
        {
            // Calculate the current percentages for each item in our portfolio
            CalculateCurrentAllocation(input.Portfolio);

            // we would like to invest in the worst maxOrderCount of the items
            return input.Portfolio
                .OrderBy(p => p.Variance)
                .Take(input.MaxOrderCount)
                .ToList();
        }

        private decimal CalculateCurrentAllocation(IEnumerable<ETFItem> items)
        {
            // get the total portfolio value
            var totalPortfolioValue = items.Sum(p => p.CurrentPrice * p.CurrentlyOwned);

            foreach (var item in items)
            {
                // calculate each actual percentage for the item
                item.ActualPercentage = 
                    totalPortfolioValue == 0 || item.CurrentlyOwned == 0
                    ? 0
                    : (item.CurrentPrice * item.CurrentlyOwned) / totalPortfolioValue;
            }

            return items.Sum(p => Math.Abs(p.Variance));
        }
    }
}
