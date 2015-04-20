using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
   public class Portfolio : RelationalBase
    {

       public string Name { get; set; }
       public int Balance { get; set; }
       public virtual ICollection<PortfolioHistory> History { get; set; } 

    }

    public class PortfolioHistory : RelationalBase
    {

        public double Change { get; set; }
        public decimal NetWorth { get; set; }
        public DateTime Time { get; set; }

        
    }

    public class Transaction : RelationalBase
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Value { get; set; }
        public decimal Price { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public virtual Share Share { get; set; }
        
    }

    public class PortfolioShares : RelationalBase
    {

        public virtual Transaction Transaction { get; set; }
        public virtual Portfolio Portfolio { get; set; }

    }


    public class UserContestPortfolioAssociation : RelationalBase
    {

        public virtual User User { get; set; }
        public virtual Contest Contest { get; set; }
        public virtual Portfolio Portfolio { get; set; }


    }
}
