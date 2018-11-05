using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IDefendantsRepository
    {
        List<vw_CaseDefectsData> GetDefendantsByCaseID(int CaseID, int SessionID);
        List<vw_CaseDefectsData> GetDefendantsByCaseID(int caseID);
        SavePartSOrder SaveOrderDefect(vw_CaseDefectData party);
        void AddDefendant(int caseID, vw_DefendantData vw_DefendantData);
        void EditDefendant(int caseID, vw_DefendantData vw_DefendantData);
        void DeleteDefendant(int CaseID, long DefendantID);
        int GetLastDefendantOrder(long caseID);
        vw_CaseDefectsData GetDefendant(int caseID, long partyID);
        bool HasDefendant(int caseID);
        bool IsPersonInCase(long personID, int caseID);
        CaseStatus CaseInFlow(int CaseID);
        string GetName(int iD);
    }
}
