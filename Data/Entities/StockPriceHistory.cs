using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class StockPriceHistory : RelationalBase
    {
        
        public double Sell { get; set; }
        public double Buy { get; set; }
        public DateTime TimeStamp { get; set; }


        // Tveksamt på dessa
        public int SellAmount { get; set; }
        public int BuyAmount { get; set; }
    }
}
