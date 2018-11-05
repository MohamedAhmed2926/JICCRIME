using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Base.Interfaces;

namespace JIC.Components.Components
{
    public class MasterCaseComponent
    {
        private IMasterCaseRepository CaseRepository;
        private IOverAllNumberRepository OverAllNumberRepository;
        private ILookupRepository LookupRepository;

        public MasterCaseComponent( IMasterCaseRepository CaseRepository, IOverAllNumberRepository OverAllNumberRepository,ILookupRepository lookupRepository)
        {
            this.CaseRepository = CaseRepository;
            this.OverAllNumberRepository = OverAllNumberRepository;
            this.LookupRepository = lookupRepository;
        }

        public void SetCaseComplete(int caseID)
        {
            CaseRepository.SetCaseCompleted(caseID);
        }

        public bool IsCaseConnectedToCircuit(int CircuitID)
        {
            return CaseRepository.IsCaseConnectedToCircuit(CircuitID);
        }

        public string CreateNationalID(vw_CaseBasicData caseBasicData)
        {
            return LookupRepository.GetNationalID(caseBasicData);
        }

        public bool IsSecondLevelNumberExistBefore(int SecondNumber, int SecondYear, int ProsecutionID,int MasterCaseID)
        {
            return CaseRepository.IsSecondLevelNumberExistBefore(SecondNumber, SecondYear, ProsecutionID,MasterCaseID);
        }
        public CaseSaveStatus AddCaseBasicData(vw_CaseBasicData caseBasicData , out int MasterCaseID)
        {
            //int ID = JIC.Repositories.DBInteractions.Utitlities.DBTranse(JIC.Crime.Repositories.DBInteractions.DBFactory.Get(), () =>
            //{

            //    if (caseBasicData.OverAllNumberProsecution > 0 && caseBasicData.OverAllNumberYear > 0)
            //    {
            //        OverallNumberComponent caseComponent = new OverallNumberComponent(caseType);

            //        caseBasicData.OverAllId = caseComponent.AddOverAll(caseBasicData);
            //    }

            //});
            //return ID;

            return CaseRepository.AddCaseBasicDate(caseBasicData , out MasterCaseID);
        }

        public void UpdateCaseBasicData(int CaseID, vw_CrimeCaseBasicData caseBasicData)
        {
            if (caseBasicData.OverAllNumberProsecution > 0 && caseBasicData.OverAllNumberYear > 0)
            {
                OverAllNumberRepository.AddOverAllNumber(caseBasicData);
            }
            UpdateCaseBasicDataVoid(CaseID, caseBasicData);
        }



        private void UpdateCaseBasicDataVoid(int CaseID, vw_CrimeCaseBasicData caseBasicData)
        {
            CaseRepository.UpdateCaseBasicData(CaseID, caseBasicData);
        }
        public void DeleteCaseBasicData( int MasterCaseID)
        {
            CaseRepository.DeleteCaseBasicData( MasterCaseID);
        }

        public IQueryable<vw_unCompletCase> GetUnComptapleCases(int courtId)
        {
            return CaseRepository.UnCompletCase(courtId);
        }
    }
}
