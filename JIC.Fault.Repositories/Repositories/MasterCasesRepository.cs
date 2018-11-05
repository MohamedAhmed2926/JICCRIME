using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.Repositories
{
    public class MasterCasesRepository : EntityRepositoryBase<Cases_MasterCase>, IMasterCaseRepository
    {
        public CaseSaveStatus AddCaseBasicDate(vw_CaseBasicData caseBasicData, out int MasterCaseID)
        {
            var FaultBasicData = (vw_FaultCaseBasicData)caseBasicData;
            var MasterCase = new Cases_MasterCase
            {
                Title = FaultBasicData.CaseName,
                FirstLevelYear = FaultBasicData.FirstYear,
                FirstLevelNumber = FaultBasicData.FirstNumber,
                ProsecutionID = FaultBasicData.SecondProsecutionID,
                SecondLevelNumber = FaultBasicData.SecondNumber,
                SecondLevelYear = FaultBasicData.SecondYear,
                CrimeID = FaultBasicData.MainCrimeID,
                NationalID = FaultBasicData.NationalID,
                PoliceStationID = FaultBasicData.FirstPoliceStationID,
                CaseTypeID = FaultBasicData.CaseTypeID
            };
            Add(MasterCase);
            Save();
            MasterCaseID = MasterCase.ID;
            return CaseSaveStatus.Saved;
        }

        public void AddOverAll(int caseID, int overAllID)
        {
            throw new NotImplementedException();
        }

        public void DeleteCaseBasicData(int MasterCaseID)
        {
            throw new NotImplementedException();
        }

        public bool IsCaseConnectedToCircuit(int CircuitID)
        {
            throw new NotImplementedException();
        }

        public bool IsSecondLevelNumberExistBefore(int SecondNumber, int SecondYear, int ProsecutionID, int MasterCaseID)
        {
            throw new NotImplementedException();
        }

        public void SetCaseCompleted(int caseID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_unCompletCase> UnCompletCase(int courtId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCaseBasicData(int caseID, vw_CaseBasicData caseBasicData)
        {
            throw new NotImplementedException();
        }
        public string CreateNationalID(vw_CaseBasicData BasicData)
        {
            var caseBasicData = (vw_FaultCaseBasicData)BasicData;
            return String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", "01", (int)CaseLevels.Initial, caseBasicData.SecondProsecutionID, caseBasicData.FirstProsecutionID, caseBasicData.CaseTypeID, caseBasicData.FirstYear, caseBasicData.FirstNumber);
        }
    }
}
