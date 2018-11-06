using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;

namespace JIC.Services.Services
{
   public class WitnessesService : ServiceBase, IWitnessesService
    {
        public WitnessesService(CaseType caseType) : base(caseType)
        {

        }

        public WitnessesComponent WitnessesComponent { get { return GetComponent<WitnessesComponent>(); } }
        public DefendantsComponent DefentantsComponent { get { return GetComponent<DefendantsComponent>(); } }
        public SessionsComponent SessionComp { get { return GetComponent<SessionsComponent>(); } }
        public VictimsComponent VictimsComponent { get { return GetComponent<VictimsComponent>(); } }
        public CircuitMembersComponent CM { get { return GetComponent<CircuitMembersComponent>(); } }
        public SessionsComponent SessionsComp { get { return GetComponent<SessionsComponent>(); } }
        public PersonComponent  PersonComp { get { return GetComponent<PersonComponent >(); } }

        public AddWitnessStatus AddNewWitness(vw_PersonData WitnessData, int CaseID,int SessionID, byte[] FileData, int UserID, SystemUserTypes UserType)
        {
            
            if (UserType == SystemUserTypes.Secretary)
            {
                // check if person is not from the circuit members
                if (CM.IsPersonIsCircuitMember((int)WitnessData.ID, CaseID))
                {
                    return AddWitnessStatus.IsCircuitMemeber;
                }
                // check if the session is not sent to judge
                if (SessionsComp.IsSentToJudge(SessionID))
                {
                    return AddWitnessStatus.IsSentToJudge;
                }
            }
          

            // check if the person is not a victim
            if (VictimsComponent.IsPersonInCase(WitnessData.ID, CaseID))
            {
                return AddWitnessStatus.IsVictim ;
            }
            // check if the person is not a defendant
            if (DefentantsComponent.IsPersonInCase(WitnessData.ID, CaseID))
            {
                return AddWitnessStatus.IsDefendant;
            }
            // Check if person didn't connected to the case before
            if (WitnessesComponent.IsConnectedToTheCase((int)WitnessData.ID, CaseID))
            {
                return AddWitnessStatus.SavedBefore;
            }

            //ToDo: check if the person is not a lawyer in the case


            long PersonID = WitnessData.ID;
            // if the person isn't in the database , add it
            if (WitnessData.ID == 0)
            {

                PersonComp.AddPerson(WitnessData,out PersonID);
            }
          

            try
            {
                 WitnessesComponent.AddNewWitness((int)PersonID , CaseID, FileData, UserID);
                return AddWitnessStatus.AddedSuccessfully;
            }
            catch (Exception e)
            {
                HandleException(e);
                return AddWitnessStatus.FailedToAdd;
            }


          

           
          
        }

        public AddTestimonyStatus AddTestimonyToWitness(int CaseID, int SessionID, string TestimonyText, int WitnessID, byte[] FileData, SystemUserTypes UserType)
        {
            if (UserType == SystemUserTypes.Secretary)
            {

               return  AddTestimonyStatus.AddedSuccessfully;
            }
            else
            {
                try
                {
                    WitnessesComponent.AddTestimonyToWitness(CaseID, WitnessID, FileData);
                    return AddTestimonyStatus.AddedSuccessfully;
                }
                catch (Exception e)
                {
                    HandleException(e);
                    return AddTestimonyStatus.FailedToAdd;
                }
              
            }
        }


        public AddWitnessStatus ConnectPersonToCaseAsWitness(int PersonID, int CaseID, int UserID, SystemUserTypes UserType)
        {
            throw new NotImplementedException();
        }

        public List<vw_CaseDefectsData> GetWitnessesByCaseID(int CaseID)
        {
            return WitnessesComponent.GetWitnessesByCaseID(CaseID);
        }

        public UpdatePresenceStatus UpdateWitnessesPresence(List<vw_WitnessAttendance> WitnessesAttendanceList)
        {
            throw new NotImplementedException();
        }
    }
}
