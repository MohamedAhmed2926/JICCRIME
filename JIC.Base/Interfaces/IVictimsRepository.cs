using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IVictimsRepository
    {
       List<vw_CaseDefectsData> GetVictimsByCaseID(int CaseID, int SessionID);
        List<vw_CaseDefectsData> GetVictimsByCaseID(int caseID);
        SavePartSOrder SaveOrderVictim(vw_CaseDefectData party);
       
        void AddVictim(int caseID, vw_VictimData vw_VictimData);
        void EditVictim(int CaseID, vw_VictimData vw_VictimData);
        void DeleteVictim(int CaseID, long VictimID);
        int GetLastVictimOrder(long caseID);
        vw_CaseDefectsData GetVictim(int caseID, long partyID);
        bool IsPersonInCase(long personID, int caseID);
    }
}
