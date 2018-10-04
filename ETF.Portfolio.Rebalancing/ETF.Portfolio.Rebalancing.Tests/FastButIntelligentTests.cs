using ETF.Portfolio.Rebalancing.Data;
using ETF.Portfolio.Rebalancing.Rebalancing;
using NUnit.Framework;
using System.Collections.Generic;

namespace ETF.Portfolio.Rebalancing.Tests
{
    [TestFixture(Category = "LV0")]
    public class FastButIntelligentTests
    {
        [Test]
        public void GivesBackACorrectResult()
        {
            var portfolio = new List<ETFItem>
            {
                new ETFItem("ISIN_1", 0, 10.0m, 0.2m),
                new ETFItem("ISIN_2", 0, 20.0m, 0.8m),
            };

            var input = new RebalanceInput(1000, 2, portfolio);
            
            var algo = new FastButIntelligentAlgo();
            var result = algo.Rebalance(input);
            Assert.AreEqual(0.0m, result.Percentage);
            Assert.AreEqual(2, result.Orders.Count);
            Assert.AreEqual(20, result.Orders[0].OrderAmount);
            Assert.AreEqual(40, result.Orders[1].OrderAmount);
        }
    }
}
