using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Services.ViewModels
{
    public class BasicShareViewModel
    {
        public Share Share { get; set; }
        public ShareHistory LatestData { get; set; }

    }

    public class DetailedShareViewModel
    {
        public Share Share { get; set; }
        public ShareHistory LatestData { get; set; }
        public List<Transaction> LatestTransactions { get; set; }

        public decimal ContestPopularity { get; set; }
        public decimal GeneralPopularity { get; set; }

    }


    public class ShareDevelopmentViewModel
    {
        public List<decimal> DevelopmentSpan { get; set; }
    }
}
