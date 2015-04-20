using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext(): base("Finance")
        {

        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        

        // Tabeller
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<ShareHistory> ShareHistory { get; set; }
        public DbSet<UserContestPortfolioAssociation> UserContestPortfolioAssociation { get; set; } 
        public DbSet<ContestSettings> ContestSettings { get; set; } 
        public DbSet<Country> Countries { get; set; } 
        public DbSet<Achievement> Achievements { get; set; } 
        public DbSet<Ranking> Rankings { get; set; } 
        public DbSet<AchievementAssociation> AchievementAssociations { get; set; } 
        
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioHistory> PortfolioHistory { get; set; }
        public DbSet<Transaction> PortfolioAssociations { get; set; }
        public DbSet<PortfolioShares> PortfolioShares { get; set; } 

    }
}
