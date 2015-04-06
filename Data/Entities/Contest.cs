using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DateTime CreationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ContestLength { get; set; }

        public int AmountOfParticipants { get; set; }
        public int CashLimit { get; set; }

        public virtual User Administrator { get; set; }
        public virtual ContestSettings Settings { get; set; }
        public virtual Image Image { get; set; }
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
