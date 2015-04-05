using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.ViewModels
{
    public class CreateContestViewModel
    {
        public string ContestType { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public int InvestmentSize { get; set; }
        
        public bool VisiblePortfolios { get; set; }
        public bool VisibleScores { get; set; }


    }
}
