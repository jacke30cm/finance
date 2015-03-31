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
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AmountOfParticipants { get; set; }
        public double CashLimit { get; set; }

        public User Admin { get; set; }

  
    }

  


}
