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
        public Country Country { get; set; }
        public string Market { get; set; }
        public string Description { get; set; }

        
        

    }


    public class Country : RelationalBase
    {
        public string Name { get; set; }
    }
}
