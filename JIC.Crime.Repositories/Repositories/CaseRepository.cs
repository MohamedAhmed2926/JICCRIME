using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class CaseRepository : EntityRepositoryBase<Cases_Cases>, ICrimeCaseRepository
    {
        public int AddCaseBasicDate(vw_CrimeCaseBasicData caseBasicData,int MasterCaseID)
        {
            var Case = new Cases_Cases
            {
                MasterCaseID = MasterCaseID,
                CaseLevelID = (int)CaseLevels.Elementary,
                CaseStatusID = caseBasicData.CaseStatusID,
                CourtID = caseBasicData.CourtID,
                ProcedureTypeID = (int)CaseProcedureTypes.Case,
                IsDeleted = false
            };
            Add(Case);
            Save();
            return Case.ID;
        }

        public void DeleteCaseBasicData(int CaseID, bool IsComplete, out int MasterCaseID)
        {
            Cases_Cases cases = GetByID(CaseID);
            MasterCaseID = cases.MasterCaseID;
            if (IsComplete)
            {
                cases.IsDeleted = true;
                Update(cases);
            }
            else
            {
                Delete(cases);
            }
          
            Save();
        }

        public vw_CrimeCaseBasicData GetCaseData(int caseID)
        {
            var Case = (from _Case in DataContext.Cases_Cases
                    join _MasterCase in DataContext.Cases_MasterCase on _Case.MasterCaseID equals _MasterCase.ID
                    join _PoliceStation in DataContext.Configurations_PoliceStations on _MasterCase.PoliceStationID equals _PoliceStation.ID
                    join _Courts in DataContext.Configurations_Courts on _Case.CourtID equals _Courts.ID
                    join prosecution in DataContext.Configurations_Prosecutions on _PoliceStation.ProsecutionID equals prosecution.ID
                    where _Case.ID == caseID && _Case.IsDeleted !=true
                 select new 
                 {
                     CaseID = _Case.ID,
                     CaseName = _MasterCase.Title,
                     CaseStatusID = _Case.CaseStatusID,
                     CourtID = _Case.CourtID,
                     FirstNumber = _MasterCase.FirstLevelNumber,
                     FirstProsecutionID = _PoliceStation.ProsecutionID,
                     FirstprosecutionName  =prosecution.Name,
                     SecoundProsecutionName = _MasterCase.Configurations_Prosecutions.Name,
                     PoliceStationID = _PoliceStation.ID,
                     FirstYear = _MasterCase.FirstLevelYear,
                     HasObtainment = _MasterCase.HasObtainments,
                     IsComplete = _MasterCase.IsComplete,
                     NationalID = _MasterCase.NationalID,
                     SecondNumber = _MasterCase.SecondLevelNumber,
                     SecondProsecutionID = _MasterCase.ProsecutionID,
                     SecondYear = _MasterCase.SecondLevelYear,
                     OverAllId = _MasterCase.Configurations_OverallNumbers != null ? _MasterCase.Configurations_OverallNumbers.ID : (int?)null,
                     OverAllNumber = _MasterCase.Configurations_OverallNumbers != null ? _MasterCase.Configurations_OverallNumbers.InclosiveSierial : (long?)null,
                     OverAllNumberProsecution = _MasterCase.Configurations_OverallNumbers != null ? _MasterCase.Configurations_OverallNumbers.ProsecutionID : (int?)null,
                     OverAllNumberYear = _MasterCase.Configurations_OverallNumbers != null ? _MasterCase.Configurations_OverallNumbers.Year : (int?)null,
                     CrimeTypeID = _MasterCase.CrimeType,
                     CrimeTypeName = _MasterCase.CrimeTypeLookup.Name,
                     MainCrimeID = _MasterCase.CrimeID,
                     MainCrimeName = _MasterCase.MainCrimeLookup.Name,
                     CourtName = _Courts.Name,
                     PoliceStationName = _PoliceStation.Name,
                    OrderOfAssignment=_MasterCase.OrderOfAssignment,
                    
                 }).First();
            return new vw_CrimeCaseBasicData
            {
                CaseID = Case.CaseID,
                CaseName = Case.CaseName,
                CaseStatusID = Case.CaseStatusID,
                CourtID = Case.CourtID,
                FirstNumberInt = int.Parse(Case.FirstNumber),
                FirstProsecutionID = Case.FirstProsecutionID,
                FirstPoliceStationID = Case.PoliceStationID,
                FirstYearInt = int.Parse(Case.FirstYear),
                HasObtainment = Case.HasObtainment,
                IsComplete = Case.IsComplete,
                NationalID = Case.NationalID,
                SecondNumberInt = int.Parse(Case.SecondNumber),
                SecondProsecutionID = Case.SecondProsecutionID.Value,
                SecondYearInt = int.Parse(Case.SecondYear),
                OverAllId = Case.OverAllId,
                OverAllNumber = Case.OverAllNumber,
                OverAllNumberProsecution = Case.OverAllNumberProsecution,
                OverAllNumberYear = Case.OverAllNumberYear,
                CrimeTypeID = Case.CrimeTypeID,
                CrimeTypeName = Case.CrimeTypeName,
                MainCrimeID = Case.MainCrimeID,
                MainCrimeName = Case.MainCrimeName,
                CourtName = Case.CourtName,
                FirstprosecutionName=Case.FirstprosecutionName,
               SecoundProsecutionName =Case.SecoundProsecutionName,
                PoliceStationName = Case.PoliceStationName,
                OrderOfAssignment=Case.OrderOfAssignment,

            };
        }

        public vw_CaseData GetCaseFullData(int caseID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_CrimeCaseBasicData> GetCasesPendingDate(int courtID)
        {
            return GetAllQuery().Where(_case => _case.Cases_CaseSessions.Count == 0 && _case.Cases_MasterCase.IsComplete && _case.IsDeleted != true).Select(_case => new vw_CrimeCaseBasicData
            {
                CaseID = _case.ID,
                CaseName = _case.Cases_MasterCase.Title,
                CaseStatusID = _case.CaseStatusID,
                CourtID = _case.CourtID,
                CourtName = _case.Configurations_Courts.Name,
                CrimeTypeID = _case.Cases_MasterCase.CrimeType,
                CrimeTypeName = _case.Cases_MasterCase.CrimeTypeLookup.Name,
                MainCrimeID = _case.Cases_MasterCase.CrimeID,
                MainCrimeName = _case.Cases_MasterCase.MainCrimeLookup.Name,
                FirstNumber = _case.Cases_MasterCase.FirstLevelNumber,
                FirstYear = _case.Cases_MasterCase.FirstLevelYear,
                FirstProsecutionID = _case.Cases_MasterCase.Configurations_PoliceStations.ProsecutionID,
                FirstPoliceStationID = _case.Cases_MasterCase.PoliceStationID,
                PoliceStationName = _case.Cases_MasterCase.Configurations_PoliceStations.Name,
                SecondNumber = _case.Cases_MasterCase.SecondLevelNumber,
                SecondYear = _case.Cases_MasterCase.SecondLevelYear,
                SecondProsecutionID = _case.Cases_MasterCase.ProsecutionID.Value,
                HasObtainment = _case.Cases_MasterCase.HasObtainments,
                IsComplete = _case.Cases_MasterCase.IsComplete,
                NationalID = _case.Cases_MasterCase.NationalID,
                OverAllId = _case.Cases_MasterCase.Configurations_OverallNumbers != null ? _case.Cases_MasterCase.Configurations_OverallNumbers.ID : (int?) null,
                OverAllNumber = _case.Cases_MasterCase.Configurations_OverallNumbers.InclosiveSierial,
                OverAllNumberProsecution = _case.Cases_MasterCase.Configurations_OverallNumbers.ProsecutionID,
                OverAllNumberYear = _case.Cases_MasterCase.Configurations_OverallNumbers.Year
            });
        }

        public int GetMasterCaseID(int caseID)
        {
            return GetByID(caseID).MasterCaseID;
        }

        public List<vw_unCompletCase> GetNotCompleteCase(int? courtId)
        {
            throw new NotImplementedException();
        }

        public bool IsCaseConnectedToCircuit(int CircuitID)
        {
            throw new NotImplementedException();
        }

        public void SendToJudge(int CaseID)
        {
           // var cases = GetByID(CaseID);
           //cases.IsSentToJudge = true;
           // //if (cases.NewCaseStatusID != null)
           // //{ cases.CaseStatusID = (int)cases.NewCaseStatusID; }
           // Update(cases);
           // Save();
        }
        public void IsSentToJudge(int CaseID)
        {
            //var cases = GetByID(CaseID);
            //cases.IsSentToJudge = true;
         
            //Update(cases);
            //Save();
        }
        public IQueryable<vw_unCompletCase> UnCompletCase(int courtId)
        {
            return (from _case in GetAllQuery()
                    join _master in DataContext.Cases_MasterCase on _case.MasterCaseID equals _master.ID
                    join _PoliceStation in DataContext.Configurations_PoliceStations on _master.PoliceStationID equals _PoliceStation.ID
                    where _case.CourtID == courtId && !_case.Cases_MasterCase.IsComplete && _case.IsDeleted != true
                    select new vw_unCompletCase
             {
                 CaseId = _case.ID,
                 CaseTitle = _master.Title,
                 CrimName = _master.CrimeTypeLookup.Name,
                 FirstNumber = _master.FirstLevelNumber,
                 FirstYear = _master.FirstLevelYear,
                 FirstprosecutionId = _PoliceStation.ProsecutionID,
                 FirstprosecutionName= _PoliceStation.Configurations_Prosecutions.Name,
                 SecondprosecutionId = _master.ProsecutionID,
                 SecondprosecutionName=_master.Configurations_Prosecutions.Name,
                 SecondNumber =_master.SecondLevelNumber,
                 SecondYear = _master.SecondLevelYear,
                 DefendantNotComplete = _case.Cases_CaseDefendants.Count == 0,
                 OrderOfAssignmentNotComplete = _case.Cases_MasterCase.OrderOfAssignment == null,
                 CaseDocumentFoldersNotComplete = _case.Cases_CaseDocumentFolders.Any(folder => folder.Cases_CaseDocuments.Count == 0) || _case.Cases_CaseDocumentFolders.Count == 0,
                 OverallNumbersNotComplete = _case.Cases_MasterCase.Configurations_OverallNumbers == null
                 

                 //? NotCompleteStatus.Defendent
                 //  : _case.Cases_MasterCase.OrderOfAssignment == null ? NotCompleteStatus.OrderOfAssignment : _case.Cases_CaseDocumentFolders.Any(folder=>folder.Cases_CaseDocuments.Count == 0) || _case.Cases_CaseDocumentFolders.Count == 0 ? NotCompleteStatus.Document  :_case.Cases_MasterCase.Configurations_OverallNumbers==null? NotCompleteStatus.OverAllNumber : NotCompleteStatus.Complete
             });
        }

        public void UpdateCaseBasicData(vw_CrimeCaseBasicData caseBasicData)
        {
            var Case = GetByID(caseBasicData.CaseID);
            Case.CaseLevelID = (int)CaseLevels.Elementary;
            Case.CaseStatusID = caseBasicData.CaseStatusID;
            Case.CourtID = caseBasicData.CourtID;
            Case.ProcedureTypeID = (int)CaseProcedureTypes.Case;

            Update(Case);
            Save();
          
        }

        public void UpdateCaseStatus(CaseStatuses  CaseStatus, int CaseID)
        {
            var Case = GetByID(CaseID);
            Case.NewCaseStatusID  = (int)CaseStatus;

            Update(Case);
            Save();

        }
        public void UpdateCaseStatus_AfterJudgeApprove( int CaseID)
        {
            var Case = GetByID(CaseID);
            if (Case.NewCaseStatusID != null)
            { Case.CaseStatusID = (int)Case.NewCaseStatusID; }
         

            Update(Case);
            Save();

        }

        public bool IsCaseReservedForJudge(int caseID)
        {
            return GetAll().Where(cases => cases.ID == caseID && cases.NewCaseStatusID  == (int)CaseStatuses.ReadyForFinalDecision).Count() > 0;
        }
    }
}
