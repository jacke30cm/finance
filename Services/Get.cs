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


                return result.OrderBy(x => x.Share.Name, StringComparer.CurrentCulture).Take(100).ToList(); 

            }

            return null; 
        }


    }
}
