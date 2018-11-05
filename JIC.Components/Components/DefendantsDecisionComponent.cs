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
    public class DefendantsDecisionComponent
    {
        public IDefendantsDecisionRepository DefendantsDecisionRepository;


        public DefendantsDecisionComponent( IDefendantsDecisionRepository DefRep) 
        {
            this.DefendantsDecisionRepository = DefRep;
        }
        public void AddDefendantDecision(vw_DefendantsDecisionData Defendants)
        {
            DefendantsDecisionRepository.AddDefendantDecision(Defendants);
        }
        public void DeleteDefendantDecision(int Session)
        {
            DefendantsDecisionRepository.DeleteDefendantDecision(Session);
        }

        public List<vw_DefendantsDecisionData> GetSessionDefendantsDecision(int SessionID)
        {
            return DefendantsDecisionRepository.GetSessionDefendantsDecision(SessionID);

        }
    }
}
