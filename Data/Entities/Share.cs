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
        public string Signature { get; set; }
        public string ListName { get; set; }

        public IList<Prices> Prices { get; set; } 

    }
}
