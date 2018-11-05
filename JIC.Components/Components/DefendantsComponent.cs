using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
    public class DefendantsComponent
    {
        private IDefendantsRepository DefentantsRepository;
        private IDefendantCaseLogRepository DefendantCaseLogRepository;
        private IDefendantChargesRepository DefendantChargesRepository;
        private IDefendantsDecisionRepository  DefDec;
        private IDefendantsSessionLogRepository  DefSess;

        public DefendantsComponent( IDefendantsRepository DefentantsRepository, IDefendantCaseLogRepository DefendantCaseLogRepository, IDefendantChargesRepository DefendantChargesRepository, IDefendantsDecisionRepository DefDec
        , IDefendantsSessionLogRepository DefSess)
        {
            this.DefentantsRepository = DefentantsRepository;
            this.DefendantCaseLogRepository = DefendantCaseLogRepository;
            this.DefendantChargesRepository = DefendantChargesRepository;

            this.DefDec = DefDec;
            this.DefSess = DefSess;
        }
        public List<vw_CaseDefectsData> GetDefendantsByCaseID(int CaseID, int SessionID) {
            return DefentantsRepository.GetDefendantsByCaseID(CaseID, SessionID);
        }

        public List<vw_CaseDefectsData> GetDefendantsByCaseID(int caseID)
        {
            return DefentantsRepository.GetDefendantsByCaseID(caseID);
        }

        public SaveDefectsStatus AddDefendant(int caseID, vw_DefendantData vw_DefendantData)
        {
            DefentantsRepository.AddDefendant(caseID, vw_DefendantData);
            DefendantCaseLogRepository.AddDefendantCaseLog(vw_DefendantData.DefendantID, vw_DefendantData.DefendantStatus, DateTime.Now);
            DefendantChargesRepository.SyncDefendantCharges(vw_DefendantData.DefendantID, vw_DefendantData.Crimes);
            return SaveDefectsStatus.Saved;
        }
        public SavePartSOrder SaveOrderDefect(vw_CaseDefectData party)
        {
            return DefentantsRepository.SaveOrderDefect(party);

        }
        public SaveDefectsStatus EditDefendant(int caseID, vw_DefendantData vw_DefendantData)
        {
            DefentantsRepository.EditDefendant(caseID, vw_DefendantData);
            DefendantCaseLogRepository.EditDefendantCaseLog(vw_DefendantData.DefendantID, vw_DefendantData.DefendantStatus, DateTime.Now);
            DefendantChargesRepository.SyncDefendantCharges(vw_DefendantData.DefendantID, vw_DefendantData.Crimes);
            return SaveDefectsStatus.Saved;
        }
        public DeleteDefectStatus DeleteDefendant(int CaseID, long DefendantID)
        {
            DefendantChargesRepository.SyncDefendantCharges(DefendantID, new List<int>());
            DefendantCaseLogRepository.DeleteDefendantCaseLog(DefendantID);
          //  DefSess.(DefendantID);
           // DefDec.DeleteDefendantDecision (DefendantID);
            DefentantsRepository.DeleteDefendant(CaseID , DefendantID);
            return DeleteDefectStatus.Deleted ;
        }
        public int GetLatestDefendantOrder(long caseID)
        {
            return DefentantsRepository.GetLastDefendantOrder(caseID);
        }

        public vw_CaseDefectsData GetCaseDefect(int caseID, long partyID)
        {
            return DefentantsRepository.GetDefendant(caseID, partyID);
        }

        public bool IsValid(int caseID)
        {
            return DefentantsRepository.HasDefendant(caseID);
        }

        public bool IsPersonInCase(long personID, int caseID)
        {
            return DefentantsRepository.IsPersonInCase(personID, caseID);
        }
        public CaseStatus CaseInFlow(int CaseID)
        {
            return DefentantsRepository.CaseInFlow(CaseID);
        }
        public string GetName(int ID)
        {
            return DefentantsRepository.GetName(ID);
        }
    }
}
