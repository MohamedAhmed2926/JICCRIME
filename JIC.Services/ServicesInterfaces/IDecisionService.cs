using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface IDecisionService
    {
        SaveDecisionStatus SaveDecision(vw_CaseDecision DecisionData);
        List<vw_KeyValueDate> GetCycleSessionDates(int SessionID);
        vw_CaseDecision GetCaseLastDecision(int CaseID, int SessionID);
        bool IsDecisionSaved(int CaseID, int SessionID);
    }
}
