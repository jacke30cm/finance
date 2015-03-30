using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }

        public Image Image { get; set; }

        public DateTime SignUp { get; set; }
        public DateTime? SignOut { get; set; }
        public DateTime? BirthDate { get; set; }

        public ICollection<Contest> ContestId { get; set; }

        public User()
        {

            ContestId = new HashSet<Contest>();

        }

    }
}
