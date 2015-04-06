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
        public decimal ChangePercent { get; set; }
        public decimal ChangeCash { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Latest { get; set; }
        public decimal Highest { get; set; }
        public decimal Lowest { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Share Share { get; set; }

    }
}
