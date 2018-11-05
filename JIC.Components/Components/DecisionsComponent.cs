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
    public class DecisionsComponent
    {
        public IDecisionsRepository DecisionsRepository;


        public DecisionsComponent( IDecisionsRepository DecisionsRepository)
        {
            this.DecisionsRepository = DecisionsRepository;
        }

     public void SaveDecision(vw_CaseDecision DecisionData )
        {
            DecisionsRepository.SaveDecision(DecisionData);
        }
        public bool IsDecisionSaved(int SessionID)
        {
          return   DecisionsRepository.IsDecisionSaved(SessionID );
        }
        public List<vw_CaseDecision> GetCaseDecisions(int CaseID)
        {
            return DecisionsRepository.GetCaseDecisions( CaseID);
        }

        public void DeleteDecision(vw_CaseDecision Decision)
        {
             DecisionsRepository.DeleteDecision(Decision);
        }
    }
}
