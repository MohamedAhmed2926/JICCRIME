using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Crime.Repositories.Repositories
{
   public class DecisionTypesRepository : EntityRepositoryBase<Configurations_DecisionTypes>, IDecisionTypesRepository
    {
        public List<vw_KeyValue> GetDecisionTypes(CaseStatuses CaseStatuses)
        {
            List<vw_KeyValue> DecisionList = new List<vw_KeyValue>();
            var allDecisions = GetAll().Where(x => x.DecisionLevel == (byte)CaseStatuses);
            foreach (Configurations_DecisionTypes obj in allDecisions)
            {
                DecisionList.Add(new vw_KeyValue { ID = obj.ID, Name = obj.Name } ) ;
            }
           return DecisionList;
        }
    }
}
