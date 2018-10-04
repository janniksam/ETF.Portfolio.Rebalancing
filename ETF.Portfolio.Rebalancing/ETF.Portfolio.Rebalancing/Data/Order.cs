using System;

namespace ETF.Portfolio.Rebalancing.Data
{
    public class Order : ICloneable
    { 
        public string ISIN { get; set; }
        
        public short OrderAmount { get; set; }

        public decimal CurrentPrice { get; set; }

        public object Clone()
        {
            return new Order
            {
                ISIN = ISIN,
                CurrentPrice = CurrentPrice,
                OrderAmount = OrderAmount
            };
        }
    }
}
