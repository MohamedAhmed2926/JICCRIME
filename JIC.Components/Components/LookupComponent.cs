using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class LookupComponent
    {
        private ILookupRepository LookupRepository;
        private ICourtRepository CourtRepository;
        public LookupComponent( ILookupRepository LookupRepository, ICourtRepository CourtRepository)
        {
            this.LookupRepository = LookupRepository;
            this.CourtRepository = CourtRepository;
        }

        public List<vw_KeyValue> GetLookup(LookupsCategories lookup)
        {
            return LookupRepository.GetLookup(lookup);
        }

        public List<vw_KeyValue> GetUserTypes()
        {
            return LookupRepository.GetUserTypes();
        }

        public List<vw_KeyValue> GetCourts()
        {
            return CourtRepository.GetCourts();
        }
        public List<vw_KeyValue> GetCourtHalls(int? CourtID)
        {
            return CourtRepository.GetCourtHalls(CourtID);
        }

        public List<vw_KeyValue> GetCaseTypes()
        {
            return LookupRepository.GetCaseTypes();
        }

        public int GetLookupIDOrCreate(LookupsCategories lookupsCategory, string nationality)
        {
            vw_KeyValue nat_lookup = GetLookup(lookupsCategory).Where(lookup => lookup.Name.Equals(nationality)).FirstOrDefault();
            if (nat_lookup == null)
                nat_lookup = LookupRepository.Create(lookupsCategory, nationality);


            return nat_lookup.ID;


        }
    }
}
