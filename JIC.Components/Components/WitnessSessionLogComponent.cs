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
   public class WitnessSessionLogComponent
    {
        public ICasesWitnessSessionLogRepository CasesWitnessSessionLogRepository;
        public WitnessSessionLogComponent(ICasesWitnessSessionLogRepository WitnessRep)
        {
            this.CasesWitnessSessionLogRepository = WitnessRep;
        }

        public UpdatePresenceStatus UpdateWitnessesPresence(List<vw_WitnessAttendance> WitnessesAttendanceList)
        {
            return CasesWitnessSessionLogRepository.UpdateWitnessesPresence(WitnessesAttendanceList);
        }
        public AddTestimonyStatus AddTestimonyToWitness(int CaseID, int SessionID, string TestimonyText, int WitnessID, byte[] FileData)
        {
            return CasesWitnessSessionLogRepository.AddTestimonyToWitness(CaseID, SessionID, TestimonyText, WitnessID, FileData);

        }
        public List<vw_WitnessAttendance> GetWitnessesByCaseID(int CaseID)
        {
            return CasesWitnessSessionLogRepository.GetWitnessesByCaseID(CaseID);

        }

    }
}
