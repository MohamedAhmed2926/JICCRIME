using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ICrimeCaseRepository
    {
        int AddCaseBasicDate(vw_CrimeCaseBasicData caseBasicData,int MasterCaseID);
        void UpdateCaseBasicData(vw_CrimeCaseBasicData caseBasicData);
        void DeleteCaseBasicData(int CaseID, bool IsComplete, out int MasterCaseID);
        IQueryable<vw_unCompletCase> UnCompletCase(int courtId);
        bool IsCaseConnectedToCircuit(int CircuitID);
        vw_CrimeCaseBasicData GetCaseData(int caseID);
        vw_CaseData GetCaseFullData(int caseID);
        int GetMasterCaseID(int caseID);
        void UpdateCaseStatus(CaseStatuses CaseStatus, int CaseID);
        IQueryable<vw_CrimeCaseBasicData> GetCasesPendingDate(int courtID);
        void SendToJudge(int CaseID);
        void UpdateCaseStatus_AfterJudgeApprove(int CaseID);
        bool IsCaseReservedForJudge(int caseID);
    }
}
