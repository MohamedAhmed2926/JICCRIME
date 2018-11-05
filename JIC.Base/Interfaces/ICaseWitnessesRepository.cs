using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Base.Interfaces
{
    public interface ICaseWitnessesRepository
    {
        List<vw_CaseDefectsData> GetWitnessesByCaseID(int CaseID);

        AddTestimonyStatus AddTestimonyToWitness(int CaseID, int SessionID, string TestimonyText, int WitnessID, byte[] FileData);

        void AddNewWitness(int PersonID, int CaseID, byte[] FileData, int UserID);

        UpdatePresenceStatus UpdateWitnessesPresence(List<vw_WitnessAttendance> WitnessesAttendanceList);

        AddWitnessStatus ConnectPersonToCaseAsWitness(int PersonID, int CaseID, int UserID, SystemUserTypes UserType);
        bool IsConnectedToTheCase(int witnessID, int caseID);
    }
}
