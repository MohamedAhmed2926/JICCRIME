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
    public class VictimsComponent
    {
        public IVictimsRepository VictimsRepository;
        public VictimsComponent( IVictimsRepository VictimsRepository)
        {
            this.VictimsRepository = VictimsRepository;
        }
        public List<vw_CaseDefectsData> GetVictimsByCaseID(int CaseID, int SessionID)
        { return VictimsRepository.GetVictimsByCaseID(CaseID, SessionID); }

        public List<vw_CaseDefectsData> GetVictimsByCaseID(int caseID)
        {
            return VictimsRepository.GetVictimsByCaseID(caseID);
        }

        public int GetLatestVictimOrder(long caseID)
        {
            return VictimsRepository.GetLastVictimOrder(caseID);
        }
       
        public SavePartSOrder SaveOrderVictim(vw_CaseDefectData party)
        {
           return VictimsRepository.SaveOrderVictim(party);
           
        }
        public SaveDefectsStatus AddVictim(int caseID, vw_VictimData vw_VictimData)
        {
            VictimsRepository.AddVictim(caseID, vw_VictimData);
            return SaveDefectsStatus.Saved;
        }

        public SaveDefectsStatus EditVictim(int caseID, vw_VictimData vw_VictimData)
        {
            VictimsRepository.EditVictim(caseID, vw_VictimData);
            return SaveDefectsStatus.Saved;
        }
        public DeleteDefectStatus DeleteVictim(int CaseID, long VictimID)
        {
            VictimsRepository.DeleteVictim(CaseID, VictimID);
            return DeleteDefectStatus.Deleted ;
        }

        public vw_CaseDefectsData GetCaseDefect(int caseID, long partyID)
        {
            return VictimsRepository.GetVictim(caseID, partyID);
        }

        public bool IsPersonInCase(long personID, int caseID)
        {
            return VictimsRepository.IsPersonInCase(personID, caseID);
        }
    }
}
