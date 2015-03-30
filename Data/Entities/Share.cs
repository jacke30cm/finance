using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Share : RelationalBase
    {
        public string Name { get; set; }
        public string Ticker { get; set; }
        public string ListName { get; set; }
        public string Market { get; set; }
        public string Description { get; set; }
        public double ChangeToday { get; set; }
        public double ChangeTodayCash { get; set; }
        public double PurchasePrice { get; set; }
        public double SellPrice { get; set; }
        public double Latest { get; set; }
        public double Highest { get; set; }
        public double Lowest { get; set; }


        public ICollection<StockPriceHistory> Prices { get; set; } 

    }
}
