using System;
using System.Data.Entity;
using JIC.Base;
using JIC.Base.Entites;

namespace JIC.Components.Components
{
    public class DatabaseComponent
    {
        private IDatabaseRepository databaseRepository;
        public DatabaseComponent( IDatabaseRepository databaseRepository)
        {
            //databaseRepository = RepositoryFactory.GetRepository<IDatabaseRepository>();
            this.databaseRepository = databaseRepository;
        }

        public DbContextTransaction BeginTransaction()
        {
            return databaseRepository.BeginTransaction();
        } 
    }
}
