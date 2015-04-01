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
        private RelationalRepository<Contest> contestRepository;
        private RelationalRepository<ContestSettings> contestSettingsRepository;
        private RelationalRepository<AvailableContestCountries> contestCountriesRepository;
        private RelationalRepository<Country> countryRepository;
        private RelationalRepository<Image> imageRepository;
        private RelationalRepository<Share> shareRepository;
        private RelationalRepository<ShareHistory> shareHistoryRepository;
        private RelationalRepository<Portfolio> portfolioRepository;
        private RelationalRepository<PortfolioHistory> portfolioHistoryRepository;
        private RelationalRepository<Transaction> transactionRepository;
        private RelationalRepository<UserContestPortfolioAssociation> portfolioAssociationRepository;


        // RelationalDB- repositories
        public RelationalRepository<User> UserRepository
        {
            get { return this.userRepository ?? new RelationalRepository<User>(context); }
        }
        public RelationalRepository<Contest> ContestRepository
        {
            get { return this.contestRepository ?? new RelationalRepository<Contest>(context); }
        }
        public RelationalRepository<ContestSettings> ContestSettingsRepository
        {
            get { return this.contestSettingsRepository ?? new RelationalRepository<ContestSettings>(context); }
        }
        public RelationalRepository<AvailableContestCountries> ContestCountriesRepository
        {
            get { return this.contestCountriesRepository ?? new RelationalRepository<AvailableContestCountries>(context); }
        }
        public RelationalRepository<Country> CountryRepository
        {
            get { return this.countryRepository ?? new RelationalRepository<Country>(context); }
        }
        public RelationalRepository<Image> ImageRepository
        {
            get { return this.imageRepository ?? new RelationalRepository<Image>(context); }
        }
        public RelationalRepository<Share> ShareRepository
        {
            get { return this.shareRepository ?? new RelationalRepository<Share>(context); }
        }
        public RelationalRepository<ShareHistory> ShareHistoryRepository
        {
            get { return this.shareHistoryRepository ?? new RelationalRepository<ShareHistory>(context); }
        }
        public RelationalRepository<Portfolio> PortfolioRepository
        {
            get { return this.portfolioRepository ?? new RelationalRepository<Portfolio>(context); }
        }
        public RelationalRepository<PortfolioHistory> PortfolioHistoryRepository
        {
            get { return this.portfolioHistoryRepository ?? new RelationalRepository<PortfolioHistory>(context); }
        }
        public RelationalRepository<Transaction> TransactionRepository
        {
            get { return this.transactionRepository ?? new RelationalRepository<Transaction>(context); }
        }
        public RelationalRepository<UserContestPortfolioAssociation> PortfolioAssociationRepository
        {
            get { return this.portfolioAssociationRepository ?? new RelationalRepository<UserContestPortfolioAssociation>(context); }
        }


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
