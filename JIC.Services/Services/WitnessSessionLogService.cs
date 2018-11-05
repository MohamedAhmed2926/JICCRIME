using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.Services
{
   public class WitnessSessionLogService : ServiceBase, IWitnessSessionLogService
    {
        public WitnessSessionLogService(CaseType caseType) : base(caseType)
        {

        }
        public WitnessSessionLogComponent WitnessSessionLogComponent { get { return GetComponent<WitnessSessionLogComponent>(); } }

        public AddTestimonyStatus AddTestimonyToWitness(int CaseID, int SessionID, string TestimonyText, int WitnessID, byte[] FileData)
        {
           return WitnessSessionLogComponent.AddTestimonyToWitness(CaseID,SessionID,TestimonyText,WitnessID,FileData);
        }

        
        public List<vw_WitnessAttendance> GetWitnessesByCaseID(int CaseID)
        {
            return WitnessSessionLogComponent.GetWitnessesByCaseID(CaseID);

        }

        public UpdatePresenceStatus UpdateWitnessesPresence(List<vw_WitnessAttendance> WitnessesAttendanceList)
        {
            return WitnessSessionLogComponent.UpdateWitnessesPresence(WitnessesAttendanceList);

        }
    }
}
