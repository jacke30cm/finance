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

                var culture = new CultureInfo("sv-SE");
                return result.OrderBy(x => x.Share.Name, StringComparer.Create(culture, false)).ToList(); 

            }

            return null; 
        }


    }
}
