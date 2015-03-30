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
        


    }
}
