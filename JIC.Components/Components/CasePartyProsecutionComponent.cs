using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
    public class CasePartyProsecutionComponent
    {
        private readonly ICaseDefendantProsecutioRepository caseDefendantProsecutioRepository;
        private readonly ICaseVictimProsecutioRepository caseVictimProsecutioRepository;

        public CasePartyProsecutionComponent(
            ICaseDefendantProsecutioRepository caseDefendantProsecutioRepository,
            ICaseVictimProsecutioRepository caseVictimProsecutioRepository
            )
        {
            this.caseDefendantProsecutioRepository = caseDefendantProsecutioRepository;
            this.caseVictimProsecutioRepository = caseVictimProsecutioRepository;
        }

        public void LinkCaseDefendantPros(long CaseDefendantID, int ProsecutionID)
        {
            caseDefendantProsecutioRepository.Create(new Base.Views.vw_CasePartyProsecution
            {
                CasePartyID = CaseDefendantID,
                ProsecutionID = ProsecutionID
            });
        }

        public void LinkCaseVictimPros(long CaseVictimID, int ProsecutionID)
        {
            caseVictimProsecutioRepository.Create(new Base.Views.vw_CasePartyProsecution
            {
                CasePartyID = CaseVictimID,
                ProsecutionID = ProsecutionID
            });
        }
    }
}
