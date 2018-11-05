using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Crime.Repositories.Repositories
{
    public class CaseTypeReposatry : EntityRepositoryBase<Configurations_CaseTypes>, ICaseTypeReposatry
    {
        public IQueryable<vw_KeyValue> GetCaseType()
        {
            return (from caseType in DataContext.Configurations_CaseTypes 
                    select new vw_KeyValue { ID=caseType.ID, Name= caseType.Name });
        }
    }
}
