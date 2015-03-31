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
       public string Balance { get; set; }
       public User PortfolioOwner { get; set; }

      
    }

    public class PortfolioHistory : RelationalBase
    {

        public double Change { get; set; }
        public double NetWorth { get; set; }
        public DateTime Time { get; set; }

        
    }

    public class Portfolioassociation : RelationalBase
    {
        
        public virtual Portfolio Portfolio { get; set; }
        public virtual Share Share { get; set; }
        
    }

    public class UserContestPortfolioAssociation : RelationalBase
    {

        public virtual User User { get; set; }
        public virtual Contest Contest { get; set; }
        public virtual Portfolio Portfolio { get; set; }

        public virtual User Admin { get; set; }


    }
}
