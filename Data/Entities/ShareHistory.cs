using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShareHistory : RelationalBase
    {
        public double ChangePercent { get; set; }
        public double ChangeCash { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Latest { get; set; }
        public double Highest { get; set; }
        public double Lowest { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Share Share { get; set; }

    }
}
