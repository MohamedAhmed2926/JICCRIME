using JIC.Base;
using JIC.Components.Components;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.Services
{
    public class ProsecutionCaseService : ServiceBase,IProsecutionCaseService
    {
        public CaseProsecutionComponent CaseProsecutionComponent { get { return GetComponent<CaseProsecutionComponent>(); } }
        public CasePartyProsecutionComponent CasePartyProsecutionComponent { get { return GetComponent<CasePartyProsecutionComponent>(); } }

        public ProsecutionCaseService(CaseType caseType) : base(caseType)
        {
        }

        public bool LinkProsCase(int ProsecutionID, int CaseID)
        {
            try
            {
                CaseProsecutionComponent.LinkCasePros(CaseID, ProsecutionID);
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
        }

        public bool LinkProsCaseParty(int ProsecutionID, long CasePartyID, PartyTypes defectType)
        {
            try
            {
                switch (defectType)
                {
                    case PartyTypes.Victim:
                        CasePartyProsecutionComponent.LinkCaseVictimPros(CasePartyID, ProsecutionID);
                        break;
                    case PartyTypes.Defendant:
                        CasePartyProsecutionComponent.LinkCaseDefendantPros(CasePartyID, ProsecutionID);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
        }
    }
}
