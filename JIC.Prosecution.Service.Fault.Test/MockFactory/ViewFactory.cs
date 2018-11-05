using JIC.Base.Views.Services;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Prosecution.Service.Fault.Test.MockFactory
{
    public class ViewFactory
    {
        private ICaseConfiguration CaseConfiguration;
        public ViewFactory(ICaseConfiguration CaseConfiguration)
        {
            this.CaseConfiguration = CaseConfiguration;

        }

        public CaseSession GetRealCaseSession(int CircuitID, int CaseTypeID)
        {
            return CaseConfiguration.GetCircuitRolls(CircuitID, CaseTypeID)
                .Select(circuitRoll => new CaseSession
                {
                    Circuit_ID = CircuitID,
                    Reservation_Code = (new Random()).Next(),
                    Session_Date = circuitRoll.Date
                }).First();
        }

        internal List<CaseParty> GetCaseParties()
        {
            return new List<CaseParty>
            {
                GetDefendant(),
                GetVictim()
            };
        }

        private CaseParty GetVictim(int order = 1)
        {
            return new CaseParty
            {
                ID = (new Random()).Next(),
                IsCivilRightProsecutor = false,
                IsLegalPerson = false,
                Name = "Victim"+ order,
                Nationality = "Egyption",
                Order = order,
                PartyType = (int)JIC.Base.PartyTypes.Victim
            };
        }

        private CaseParty GetDefendant(int order = 1)
        {
            return new CaseParty
            {
                ID = (new Random()).Next(),
                IsCivilRightProsecutor = false,
                IsLegalPerson = false,
                Name = "Defendant" + order,
                Nationality = "Egyption",
                Order = order,
                PartyType = (int)JIC.Base.PartyTypes.Defendant,
                DefendantCharges = new List<int> { 1 },
                DefendantPoliceStationStatusID = 21
            };
        }
    }
}
