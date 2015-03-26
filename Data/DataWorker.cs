using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data
{
    public class DataWorker : IDisposable
    {
        private DataContext context = new DataContext();



        // RelationalDB
        private RelationalRepository<User> userRepository;

        // MongoDB



        // RelationalDB- repositories
        public RelationalRepository<User> UserRepository
        {
            get { return this.userRepository ?? new RelationalRepository<User>(context); }
        }




        // MongoRepositories

        public void Save()
        {
            context.SaveChanges();
        }

        private bool Disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.Disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
