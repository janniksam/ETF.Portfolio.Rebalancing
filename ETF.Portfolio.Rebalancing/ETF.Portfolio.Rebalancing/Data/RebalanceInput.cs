using System.Collections.Generic;

namespace ETF.Portfolio.Rebalancing.Data
{
    public class RebalanceInput
    {
        /// <summary>
        /// Set some parameters to be used by the Rebalance-Algos
        /// </summary>
        /// <param name="moneyToSpent">The total sum of money you wish to spend</param>
        /// <param name="maxOrderCount">
        /// The maximum number of items you would like to order. To minimize the payment fees,
        /// I would recommend to order as few as necessary, although the best result will the most times
        /// be the calculation with maximum order count (length of <paramref name="portfolio"/>)
        /// </param>
        /// <param name="portfolio">Your current portfolio and the allocations you wish to fulfill</param>
        public RebalanceInput(decimal moneyToSpent, short maxOrderCount, List<ETFItem> portfolio)
        {
            MoneyToSpent = moneyToSpent;
            MaxOrderCount = maxOrderCount;
            Portfolio = portfolio;
        }

        public decimal MoneyToSpent { get; private set; }

        public short MaxOrderCount { get; private set; }

        public IList<ETFItem> Portfolio { get; private set; }
    }
}
