using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IDecisionsRepository
    {
        void SaveDecision(vw_CaseDecision DecisionData);
        bool IsDecisionSaved(int SessionID);
        List<vw_CaseDecision> GetCaseDecisions(int CaseID);
        void DeleteDecision(vw_CaseDecision Decision);
    }
}
