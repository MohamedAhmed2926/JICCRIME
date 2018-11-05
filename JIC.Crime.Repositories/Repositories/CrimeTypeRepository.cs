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
    public class CrimeTypeRepository : EntityRepositoryBase<Cases_CrimeTypes>, ICrimeTypeRepository
    {
        public IQueryable<vw_KeyValue> GetCrimType(int? CourtID)
        {
            if (!CourtID.HasValue)
                return (from CrimeTypes in DataContext.Cases_CrimeTypes where CrimeTypes.Name != "جنح صحفية"
                        select new vw_KeyValue { ID = CrimeTypes.ID, Name = CrimeTypes.Name });
            else
                return (from CrimeType in DataContext.Cases_CrimeTypes
                        join CrimeTypeCourt in DataContext.Configurations_Courts_Crimes on CrimeType.ID equals CrimeTypeCourt.CrimeTypeID
                        where CrimeTypeCourt.CourtID == CourtID.Value && CrimeType.Name != "جنح صحفية"
                        select new vw_KeyValue { ID = CrimeType.ID, Name = CrimeType.Name });
        }
    }
}
