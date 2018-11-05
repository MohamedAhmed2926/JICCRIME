using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class CrimeCaseComponent
    {
        #region Repository
        public ICrimeCaseRepository CaseRepository;
        #endregion
        public CrimeCaseComponent(ICrimeCaseRepository CaseRepository)
        {
            this.CaseRepository = CaseRepository;
        }

  

        public int AddCaseData(vw_CrimeCaseBasicData caseBasicData, int masterCaseID)
        {
            return CaseRepository.AddCaseBasicDate(caseBasicData,masterCaseID);
        }


        public vw_CrimeCaseBasicData GetCaseData(int caseID)
        {
            return CaseRepository.GetCaseData(caseID);
        }
        
        public vw_CaseData GetCaseFullData(int caseID)
        {
            return CaseRepository.GetCaseFullData(caseID);
        }

        public void UpdateCaseBasicData( vw_CrimeCaseBasicData caseBasicData)
        {
             CaseRepository.UpdateCaseBasicData(caseBasicData);
        }
        public bool DeleteCaseBasicData(int CaseID, bool IsComplete, out int MasterCaseID)
        {
            CaseRepository.DeleteCaseBasicData(CaseID ,IsComplete, out MasterCaseID);
            return true;
        }
        public bool IsValidCase(int caseID)
        {
            throw new NotImplementedException();
        }

        public int GetMasterCaseID(int caseID)
        {
            return CaseRepository.GetMasterCaseID(caseID);
        }

        public void UpdateCaseStatus(CaseStatuses  CaseStatus, int CaseID)
        {
            CaseRepository.UpdateCaseStatus(CaseStatus, CaseID);

        }
        public void UpdateCaseStatus_AfterJudgeApprove(int CaseID)
        {
            CaseRepository.UpdateCaseStatus_AfterJudgeApprove(CaseID);

        }
        public IQueryable<vw_CrimeCaseBasicData> GetAllCasesPendingDate(int courtID)
        {
            return CaseRepository.GetCasesPendingDate(courtID);
        }

        public void SendSessionToJudge(int CaseID)
        {
            CaseRepository.SendToJudge(CaseID);
        }

        public bool IsCaseReservedForJudge(int caseID)
        {
           return CaseRepository.IsCaseReservedForJudge(caseID);
        }
    }
}
