using System;

namespace ETF.Portfolio.Rebalancing.Data
{
    public class ETFItem : ICloneable, IComparable
    {
        public ETFItem(string isin, short currentlyOwned, decimal currentPrice, decimal shouldPercentage)
        {
            ISIN = isin;
            CurrentlyOwned = currentlyOwned;
            CurrentPrice = currentPrice;
            ShouldPercentage = shouldPercentage;
        }

        public string ISIN { get; private set; }

        public decimal CurrentPrice { get; private set; }

        public decimal ShouldPercentage { get; private set; }

        public decimal Variance
        {
            get
            {
                return ActualPercentage - ShouldPercentage;
            }
        }

        public short CurrentlyOwned { get; set; }

        public decimal ActualPercentage { get; set; }

        public object Clone()
        {
            return new ETFItem(ISIN, CurrentlyOwned, CurrentPrice, ShouldPercentage);
        }

        public int CompareTo(object obj)
        {
            var etf = (ETFItem)obj;
            return ISIN.CompareTo(etf.ISIN);
        }
    }
}