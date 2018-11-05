using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
   public class DecisionTypesComponent
    {
        public IDecisionTypesRepository DecisionTypesRepository;

        public DecisionTypesComponent( IDecisionTypesRepository DecisionTypesRepository)
        {
            this.DecisionTypesRepository = DecisionTypesRepository;
        }

        public List<vw_KeyValue> GetDecisionTypes(CaseStatuses CaseStatuses)
        {
            return DecisionTypesRepository.GetDecisionTypes(CaseStatuses);
        }

    }
}
