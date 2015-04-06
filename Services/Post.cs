using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Services.ViewModels;

namespace Services
{
    public class Post : ServiceBase
    {

        public long CreateContest(CreateContestPostModel model, string uploadedFile, string userId)
        {
            var admin = uow.UserRepository.GetSingle(x => x.Id.Equals(userId));
            var image = new Image()
            {
                Url = uploadedFile
            }; 

            uow.ImageRepository.Add(image);
            uow.Save();

            var contest = new Contest()
            {
                Name = model.Name,
                Administrator = admin,
                AmountOfParticipants = model.Participants,
                CashLimit = model.CashLimit,
                ContestType = model.ContestType,
                Description = model.Description,
                CreationDate = DateTime.Now,
                ContestLength = model.ContestLength,
                Image =  image,
                Settings = new ContestSettings()
                {
                    VisiblePortfolios = model.VisiblePortfolios,
                    VisibleScore = model.VisibleScores
                }

            }; 

            uow.ContestRepository.Add(contest);
            uow.Save();

            return contest.Id; 
        }





    }
}
