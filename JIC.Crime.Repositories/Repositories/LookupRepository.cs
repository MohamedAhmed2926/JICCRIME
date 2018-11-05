using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class LookupRepository : EntityRepositoryBase<Configurations_Lookups>, ILookupRepository
    {
        public vw_KeyValue Create(LookupsCategories lookupsCategory, string nationality)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCaseTypes()
        {
            throw new NotImplementedException();
        }

        public List<DecisionTypes> GetDecisionTypes()
        {
            throw new NotImplementedException();
        }

        public List<DecisionTypes> GetJudgeOrders_Elementary()
        {
            throw new NotImplementedException();
        }

        public List<DecisionTypes> GetJudgeOrders_Initial()
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetLookup(LookupsCategories lookupCategory)
        {
            if (lookupCategory == LookupsCategories.JudgLevel)
            {
                var list= this.GetAllQuery().Where(lookup => lookup.CategoryID == (int)lookupCategory).Select(lookup => new vw_KeyValue
                {
                    ID = lookup.ID,
                    Name = lookup.Name
                }).ToList();

              return  list.Where(z => z.ID == 39 || z.ID == 40 || z.ID == 41).ToList();

            }
            else
            {
                return this.GetAllQuery().Where(lookup => lookup.CategoryID == (int)lookupCategory).Select(lookup => new vw_KeyValue
                {
                    ID = lookup.ID,
                    Name = lookup.Name
                }).ToList();
            }
        }

        public string GetNationalID(vw_CaseBasicData BasicData)
        {
            var caseBasicData = (vw_CrimeCaseBasicData)BasicData;
            return String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", "02", (int)CaseLevels.Elementary, caseBasicData.SecondProsecutionID, caseBasicData.FirstProsecutionID, caseBasicData.CrimeTypeID, caseBasicData.FirstYearInt, caseBasicData.FirstNumberInt);
        }

        public List<vw_KeyValue> GetUserTypes()
        {
            return new List<vw_KeyValue>
            {
                new vw_KeyValue((int)SystemUserTypes.JICAdmin,"مدير نظام مركز معلومات  القضائى "),
                new vw_KeyValue((int)SystemUserTypes.ElementaryCourtAdministrator,"مدير نظام المحكمة"),
                new vw_KeyValue((int)SystemUserTypes.schedualEmployee,"مراجع جدول"),
                //new vw_KeyValue((int)SystemUserTypes.ImplementationEmployee,"موظف التنفيذ"),
                new vw_KeyValue((int)SystemUserTypes.Secretary,"أمين السر"),
                new vw_KeyValue((int)SystemUserTypes.Judge,"القاضى"),
                //new vw_KeyValue((int)SystemUserTypes.CourtHead,"رئيس المحكمة"),
                //new vw_KeyValue((int)SystemUserTypes.InquiriesEmployee,"موظف استعلام"),
            };
        }
    }
}
