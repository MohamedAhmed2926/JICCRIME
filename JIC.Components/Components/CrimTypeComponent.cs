using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;

namespace JIC.Components.Components
{
    public class CrimTypeComponent
    {
        public ICrimeTypeRepository CrimeTypeRepository;
        public CrimTypeComponent( ICrimeTypeRepository CrimeTypeRepository)
        {
            this.CrimeTypeRepository = CrimeTypeRepository;
        }
        public IQueryable<JIC.Base.Views.vw_KeyValue> GetCrimeType(int? CourtID)
        {
            return CrimeTypeRepository.GetCrimType(CourtID);
        }
    }
}
