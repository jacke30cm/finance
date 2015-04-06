using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Contest : RelationalBase
    {
        public string Name { get; set; }
        public string ContestType { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public int AmountOfParticipants { get; set; }
        public int CashLimit { get; set; }
        public User Admin { get; set; }
        public ContestSettings Settings { get; set; }
        public Image Image { get; set; }
    }


    public class ContestSettings : RelationalBase
    {
        public bool VisiblePortfolios { get; set; }
        public bool VisibleScore { get; set; }

    }

    public class AvailableContestCountries
    {
        public virtual Contest Contest { get; set; }
        public virtual Country Country { get; set; }
    }

   

}
