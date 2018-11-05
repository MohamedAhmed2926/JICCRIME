using JIC.Base.Entites;
using JIC.Crime.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class DatabaseRepository : RepositoryBase,IDatabaseRepository
    {
        public DbContextTransaction BeginTransaction()
        {
            if (DataContext.Database.CurrentTransaction == null)
                return DataContext.Database.BeginTransaction();
            return null;
        }
    }
}
