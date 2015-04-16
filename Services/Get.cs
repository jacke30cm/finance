using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Services.ViewModels;

namespace Services
{
    public class Get : ServiceBase
    {

        public bool EmailAvailability(string email)
        {
           return uow.UserRepository.GetSingle(x => x.Email.Equals(email)) == null;
        }

        public BasicContestViewModel GetBasicContestData(long id)
        {
            var contest = uow.ContestRepository.GetSingle(x => x.Id == id);
            
            return new BasicContestViewModel()
            {
                Name = contest.Name,
                EndTime = contest.EndDate,
                Image = uow.ImageRepository.GetSingle(x => x.Id == contest.Image.Id)
            }; 

        }


        public UserViewModel GetUser(string userId)
        {
            var user = uow.UserRepository.GetSingle(x => x.Id.Equals(userId));
            if (user == null) return null;

            if (user.Image != null)
            {
                var image = uow.ImageRepository.GetSingle(x => x.Id == user.Image.Id);
                return new UserViewModel
                {
                    User = user,
                    Image = image
                };
            }

            return new UserViewModel
            {
                User = user,
            };
        }


        public List<BasicShareViewModel> GetBasicShareData()
        {
            var shares = uow.ShareRepository.Get();
            var result = new Collection<BasicShareViewModel>(); 

            if (shares != null)
            {
                foreach (var share in shares)
                {
                    var item = new BasicShareViewModel()
                    {
                        Share = share,
                        LatestData = uow.ShareHistoryRepository.GetSingle(x => x.Share.Id == share.Id)
                    };

                    result.Add(item);
                }


                return result.OrderBy(x => x.Share.Name, StringComparer.CurrentCulture).ToList(); 

            }

            return null; 
        }


        public DetailedShareViewModel GetDetailedShareData(long id)
        {
            var share = uow.ShareRepository.GetSingle(x => x.Id == id);
            if (share == null) return null;

            // Get 5 latest transactions
            var transactions =
                uow.TransactionRepository.Get(x => x.Share.Id == share.Id).OrderByDescending(x => x.TimeStamp).Take(5).ToList();


            // Get latest bid & ask
            var data = uow.ShareHistoryRepository.Get(x => x.Share.Id == share.Id).OrderByDescending(x => x.TimeStamp).SingleOrDefault();

            return new DetailedShareViewModel()
            {
                Share = share,
                LatestData = data,
                LatestTransactions = transactions

            };


        }

        public int CalculateLocalPopularity(long contestId, long shareId)
        {
            return 0; 
        }

        public int CalculcateGeneralPopularity()
        {
            return 0; 
        }

        public List<Contest> GetContestByUser(string userId)
        {
            var usr = uow.UserRepository.GetByID(userId);
            var contestList = new List<Contest>();

            var userContests = uow.PortfolioAssociationRepository.Get(x => x.User.Id == usr.Id);

            foreach (var element in userContests)
            {
                contestList.Add(uow.ContestRepository.GetSingle(x => x.Id == element.Contest.Id));
            }


            return contestList;

        }


    }
}
