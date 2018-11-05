using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
   public interface ICrimeCaseService
    {
        CaseSaveStatus AddBasicData(vw_CrimeCaseBasicData caseBasicData, out int CaseID);
        CaseSaveStatus UpdateBasicData(vw_CrimeCaseBasicData caseBasicData);
        DeleteStatus DeleteBasicData(int CaseID);
        vw_CaseData GetCaseData(int CaseID);
        vw_CrimeCaseBasicData GetCaseBasicData(int CaseID);
        IQueryable<vw_CrimeCaseBasicData> GetAllCasesPendingDate(int CourtID);
        void UpdateCaseStatus_AfterJudgeApprove(int CaseID);


    }
}
