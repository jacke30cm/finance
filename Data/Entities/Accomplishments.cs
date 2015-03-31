using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Achievement : RelationalBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
    }

    public class Ranking : RelationalBase
    {
        public int Score { get; set; }
    }


    public class AchievementAssociation : RelationalBase
    {
        public virtual User User { get; set; }
        public virtual Achievement Achievement { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
