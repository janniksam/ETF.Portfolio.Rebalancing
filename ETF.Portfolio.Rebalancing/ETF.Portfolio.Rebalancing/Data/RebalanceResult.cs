using System.Collections.Generic;

namespace ETF.Portfolio.Rebalancing.Data
{
    public class RebalanceResult
    {
        public RebalanceResult()
        {
            Percentage = decimal.MaxValue;
        }

        /// <summary>
        /// The sum of variances of your portfolio after you order
        /// </summary>
        public decimal Percentage { get; internal set; }

        /// <summary>
        /// The orders you should make to reach the calculated percentage
        /// </summary>
        public IList<Order> Orders { get; internal set; }
    }
}