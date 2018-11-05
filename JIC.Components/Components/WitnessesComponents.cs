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
   public class WitnessesComponent
    {
        public ICaseWitnessesRepository  CaseWitnessesRepository;

        public WitnessesComponent(ICaseWitnessesRepository WitnessRep)
        {
            this.CaseWitnessesRepository = WitnessRep;
        }


        public void AddNewWitness(int PersonID, int CaseID, byte[] FileData, int UserID)
        {
            CaseWitnessesRepository.AddNewWitness(PersonID , CaseID, FileData, UserID);
        }

        public void AddTestimonyToWitness(int CaseID,int WitnessID, byte[] FileData)
        {
            
        }


        public AddWitnessStatus ConnectPersonToCaseAsWitness(int PersonID, int CaseID, int UserID, SystemUserTypes UserType)
        {
            throw new NotImplementedException();
        }

        public List<vw_CaseDefectsData> GetWitnessesByCaseID(int CaseID)
        {
            return CaseWitnessesRepository.GetWitnessesByCaseID(CaseID);
        }

        public UpdatePresenceStatus UpdateWitnessesPresence(List<vw_WitnessAttendance> WitnessesAttendanceList)
        {
            throw new NotImplementedException();
        }

        public bool IsConnectedToTheCase(int WitnessID, int CaseID)
        {
            return CaseWitnessesRepository.IsConnectedToTheCase(WitnessID, CaseID);
        }
    }
}
