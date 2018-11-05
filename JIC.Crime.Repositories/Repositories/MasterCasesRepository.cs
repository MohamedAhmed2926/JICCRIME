using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;
using JIC.Repositories.DBInteractions;
using JIC.Crime.Entities.Models;
using JIC.Base;

namespace JIC.Crime.Repositories.Repositories
{
    public class MasterCasesRepository : EntityRepositoryBase<Cases_MasterCase>, IMasterCaseRepository
    {
        public bool IsSecondLevelNumberExistBefore(int SecondNumber, int SecondYear, int ProsecutionID , int MasterCaseID)
        {
           return DataContext.Cases_MasterCase.Where(x => x.SecondLevelNumber == SecondNumber.ToString()
                    && x.SecondLevelYear == SecondYear.ToString() && x.ProsecutionID == ProsecutionID &&x.ID!=MasterCaseID).Count() > 0;


        }
        public CaseSaveStatus AddCaseBasicDate(vw_CaseBasicData _caseBasicData, out int MasterCaseID)
        {
            try
            {
                bool found = (DataContext.Cases_MasterCase.Where(x => x.SecondLevelNumber == _caseBasicData.SecondNumber.ToString()
                     && x.SecondLevelYear == _caseBasicData.SecondYear.ToString() && x.ProsecutionID == _caseBasicData.SecondProsecutionID).Count() > 0);

                if (found)
                {
                    MasterCaseID = 0;
                    return CaseSaveStatus.SecondNumberExistBefore;
                }
                else
                {
                    Cases_MasterCase cases = InitializeMasterCase(_caseBasicData);
                    this.Add(cases);
                    this.Save();
                    MasterCaseID = cases.ID;
                    return CaseSaveStatus.Saved;
                }
            }
            catch (Exception)
            {
                MasterCaseID = 0;
                return CaseSaveStatus.Failed;
            }
        }

        private static Cases_MasterCase InitializeMasterCase(vw_CaseBasicData BasicData)
        {
            var caseBasicData = (vw_CrimeCaseBasicData)BasicData;
            Cases_MasterCase cases = new Cases_MasterCase()
            {

                Title = caseBasicData.CaseName,
                FirstLevelYear = caseBasicData.FirstYearInt.ToString(),
                FirstLevelNumber = caseBasicData.FirstNumberInt.ToString(),
                ProsecutionID = caseBasicData.SecondProsecutionID,
                SecondLevelNumber = caseBasicData.SecondNumberInt.ToString(),
                SecondLevelYear = caseBasicData.SecondYearInt.ToString(),
                CrimeID = caseBasicData.MainCrimeID,
                CrimeType = caseBasicData.CrimeTypeID,

                HasObtainments = caseBasicData.HasObtainment,
                NationalID = caseBasicData.NationalID,
                PoliceStationID = caseBasicData.FirstPoliceStationID,
                OrderOfAssignment=caseBasicData.OrderOfAssignment,
               OverallID=caseBasicData.OverAllId,
               IsComplete=caseBasicData.IsComplete,


            };
            if (caseBasicData.OverAllId.HasValue)
            {
                cases.OverallID = caseBasicData.OverAllId.Value;
            }

            return cases;
        }

        public IQueryable<vw_unCompletCase> UnCompletCase(int courtId)
        {
            var result = (from cases in DataContext.Cases_MasterCase
                          join crims in DataContext.Cases_CrimeTypes on cases.CrimeID equals crims.ID
                          join pres in DataContext.Configurations_Prosecutions on cases.ProsecutionID equals pres.ID
                          join overOll in DataContext.Configurations_OverallNumbers on cases.OverallID equals overOll.ID into overAllG
                          from overAllFinal in overAllG.DefaultIfEmpty()
                          where cases.IsComplete == false && pres.CourtID == courtId
                          select new vw_unCompletCase
                          {
                              CaseId = cases.ID,
                              CaseTitle = cases.Title,
                              CrimName = crims.Name,
                              FirstNumber = cases.FirstLevelNumber,
                              FirstYear = cases.FirstLevelYear,
                              SecondYear = cases.SecondLevelYear,
                              OverAll = overAllFinal == null ? 0 : overAllFinal.InclosiveSierial

                          });
            return result;
        }

        public void UpdateCaseBasicData(int MasterCaseID, vw_CaseBasicData caseBasicData)
        {
            Cases_MasterCase cases = InitializeMasterCase(caseBasicData);
            cases.ID = MasterCaseID;
            Update(cases);
            Save();
        }
        public void DeleteCaseBasicData(int MasterCaseID)
        {
            Cases_MasterCase cases = GetByID(MasterCaseID);

            Delete(cases);
            Save();
        }
        public bool IsCaseConnectedToCircuit(int CircuitID)
        {
            return DataContext.Cases_Cases.Where(Case => Case.CircuitID == CircuitID).Count() > 0;
        }

        public void SetCaseCompleted(int caseID)
        {
            Cases_MasterCase MasterCase = GetMasterCaseFromCaseID(caseID);
            MasterCase.IsComplete = true;
            Update(MasterCase);
            Save();
        }
        public void AddOverAll(int caseID, int overAllID)
        {
            var MasterCase = GetMasterCaseFromCaseID(caseID);
            MasterCase.OverallID = overAllID;
            Update(MasterCase);
            Save();
        }


        #region Helper
        private Cases_MasterCase GetMasterCaseFromCaseID(int caseID)
        {
            return GetAllQuery().Where(master => master.Cases_Cases.Any(_case => _case.ID == caseID && _case.IsDeleted != true)).Select(master => master).First();
        }
        #endregion

    }
}
