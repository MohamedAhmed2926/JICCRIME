using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.Repositories
{
    public class LookupRepository : EntityRepositoryBase<Configurations_Lookups>, ILookupRepository
    {
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
            return this.GetAllQuery().Where(lookup => lookup.CategoryID == (int)lookupCategory).Select(lookup => new vw_KeyValue
            {
                ID = lookup.ID,
                Name = lookup.Name
            }).ToList();
        }

        public string GetNationalID(vw_CaseBasicData BasicData)
        {
            var caseBasicData = (vw_FaultCaseBasicData)BasicData;
            return String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", SystemConfigurations.Defaults_CurrentCourtType, CaseLevels.Initial.Code(), caseBasicData.SecondProsecutionID, caseBasicData.FirstProsecutionID, GetCaseTypeCode(caseBasicData.CaseTypeID), caseBasicData.FirstYear, caseBasicData.FirstNumber);
        }

        private string GetCaseTypeCode(int CaseTypeID)
        {
            var code = this.DataContext.Configurations_CaseTypes.Where(caseType => caseType.ID == CaseTypeID).Select(caseType=>caseType.Code).FirstOrDefault();
            return string.IsNullOrEmpty(code) ? "00" : code;
        }
        public List<vw_KeyValue> GetUserTypes()
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCaseTypes()
        {
            return DataContext.Configurations_CaseTypes.Select(caseType => new vw_KeyValue { ID = caseType.ID, Name = caseType.Name }).ToList();
        }

        public vw_KeyValue Create(LookupsCategories lookupsCategory, string name)
        {
            Configurations_Lookups Lookup = new Configurations_Lookups
            {
                CategoryID = (int)lookupsCategory,
                Name = name
            };
            base.Add(Lookup);
            Save();
            return new vw_KeyValue
            {
                ID = Lookup.ID,
                Name = Lookup.Name
            };
        }
    }
}
