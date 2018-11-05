using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;

namespace JIC.Components.Components
{
   public class OverallNumberComponent
    {
        private IOverAllNumberRepository OverAllNumberRepository;
        private IMasterCaseRepository MasterCaseRepository;
        private ICrimeCaseRepository CaseRepository;
        
        public OverallNumberComponent( IOverAllNumberRepository OverAllNumberRepository,IMasterCaseRepository masterCaseRepository,ICrimeCaseRepository caseRepository)
        {
            this.OverAllNumberRepository = OverAllNumberRepository;
            this.MasterCaseRepository = masterCaseRepository;
            this.CaseRepository = caseRepository;
        }
        
        public int AddOverAll(JIC.Base.Views.vw_CrimeCaseBasicData vw_CaseBasicData)
        {
            return OverAllNumberRepository.AddOverAllNumber(vw_CaseBasicData);
        }

        public bool AddOverAll(int caseID, out long number, out int prosecutionID, out int year)
        {
            int OverAllID;
            if (OverAllNumberRepository.AddOverAllNumber(caseID, out number, out prosecutionID, out year, out OverAllID))
            {
                MasterCaseRepository.AddOverAll(caseID, OverAllID);
                return true;
            }
            return false;
        }

        public AddOverAllStatus EditOverAll(int caseID, long number,int prosecutionID, int year)
        {
            if (OverAllNumberRepository.IsOverAllEmpty(number, prosecutionID, year))
            {
                OverAllNumberRepository.DisableOverAll(caseID);
                int OverAllID = OverAllNumberRepository.GetOverAllID(number, prosecutionID, year, CaseRepository.GetCaseData(caseID).CourtID);
                MasterCaseRepository.AddOverAll(caseID, OverAllID);
            }
            return AddOverAllStatus.OverAllReserved;
        }

        public void DisableOverAll(int caseID)
        { OverAllNumberRepository.DisableOverAll(caseID); }
    }
}
