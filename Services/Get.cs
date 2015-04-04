using System;
using System.Collections.Generic;
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


    }
}
