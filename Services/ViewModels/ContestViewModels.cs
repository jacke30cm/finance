using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data.Entities;

namespace Services.ViewModels
{
    public class CreateContestPostModel
    {
        public string ContestType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int CashLimit { get; set; }
        
        public bool VisiblePortfolios { get; set; }
        public bool VisibleScores { get; set; }


    }


    public class BasicContestViewModel
    {
        public string Name { get; set; }
        public DateTime EndTime { get; set; }
        public Image Image { get; set; }

        // maybe more stuff later

    }

}   


