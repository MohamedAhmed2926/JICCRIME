using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Crime.View.TestInterfaces
{
    public class CaseBasicDataService : ICrimeCaseService
    {
        //public CaseSaveStatus AddBasicData(vw_CaseBasicData caseBasicData, out int CaseID)
        //{
        //    CaseID = 1;
        //    return CaseSaveStatus.Saved;
        //}

        public CaseSaveStatus AddBasicData(vw_CrimeCaseBasicData caseBasicData, out int CaseID)
        {
            throw new NotImplementedException();
        }

        //public CaseSaveStatus AddBasicData(vw_CrimeCaseBasicData caseBasicData, out int CaseID)
        //{
        //    throw new NotImplementedException();
        //}

        public void AddCaseDefendant(int CaseID, vw_DefendantData DefendantData, out int DefendantID)
        {
            throw new NotImplementedException();
        }

        public void AddCaseDescription(vw_CaseDescription CaseDescription, int CaseID)
        {
            throw new NotImplementedException();
        }

        public void AddCaseVictim(int CaseID, vw_VictimData vw_VictimData, out int VictimID)
        {
            throw new NotImplementedException();
        }

        public CaseSaveStatus AddFaultCaseBasicData(vw_CaseBasicData caseBasicData, out int CaseID)
        {
            throw new NotImplementedException();
        }

        public DeleteStatus DeleteBasicData(int CaseID)
        {
            throw new NotImplementedException();
        }

        public void DeleteCaseDefendant(int DefendantID)
        {
            throw new NotImplementedException();
        }

        public void DeleteCaseVictim(int DefendantID)
        {
            throw new NotImplementedException();
        }

        public bool DownloadDocument(int DocumentID)
        {
            return true;
        }

        public void EndCase(int CaseID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_CrimeCaseBasicData> GetAllCasesPendingDate(int CourtID)
        {
            throw new NotImplementedException();
        }

        public vw_CrimeCaseBasicData GetCaseBasicData(int CaseID)
        {
            throw new NotImplementedException();
        }

        public vw_CaseData GetCaseData(int CaseID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_unCompletCase> GetUnCompletCases(int courtId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_unCompletCase> UnComplete(int CourtId)
        {
            throw new NotImplementedException();
        }

        public SaveStatus UpdateBasicData(vw_CrimeCaseBasicData caseBasicData, int CaseID)
        {
            throw new NotImplementedException();
        }

        public SaveStatus UpdateBasicData(vw_CrimeCaseBasicData caseBasicData)
        {
            throw new NotImplementedException();
        }

        public void UpdateCaseStatus_AfterJudgeApprove(int CaseID)
        {
            throw new NotImplementedException();
        }

        //bool ICrimeCaseService.AddCaseDescription(vw_CaseDescription CaseDescription, int CaseID)
        //{
        //    throw new NotImplementedException();
        //}

        CaseSaveStatus ICrimeCaseService.UpdateBasicData(vw_CrimeCaseBasicData caseBasicData)
        {
            throw new NotImplementedException();
        }

        //SaveStatus ICaseService.AddBasicData(vw_CaseBasicData caseBasicData, out int CaseID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}