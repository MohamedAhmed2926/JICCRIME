using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IDefendantsDecisionRepository
    {
        void AddDefendantDecision(vw_DefendantsDecisionData Defendants);
        void DeleteDefendantDecision(int session);
        List<vw_DefendantsDecisionData> GetSessionDefendantsDecision(int SessionID);
    }
}
