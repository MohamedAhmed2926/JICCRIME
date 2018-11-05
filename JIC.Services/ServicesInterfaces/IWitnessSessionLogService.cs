using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
  public  interface IWitnessSessionLogService
    {
        List<vw_WitnessAttendance> GetWitnessesByCaseID(int CaseID);

        AddTestimonyStatus AddTestimonyToWitness(int CaseID, int SessionID, string TestimonyText, int WitnessID, byte[] FileData);

     //   AddWitnessStatus AddNewWitness(vw_PersonData WitnessData, int CaseID, int SessionID, byte[] FileData, int UserID, SystemUserTypes UserType);

        UpdatePresenceStatus UpdateWitnessesPresence(List<vw_WitnessAttendance> WitnessesAttendanceList);
    }
}
