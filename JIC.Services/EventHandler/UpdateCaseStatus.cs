using JIC.Base;
using JIC.Components.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.EventHandler
{
    public class UpdateCaseStatus : IEventBus<CaseDessionSaved>
    {
        public CrimeCaseComponent CaseComp { get; set; }
        public UpdateCaseStatus(CrimeCaseComponent caseComponent)
        {
            this.CaseComp = caseComponent;
        }
        public void Handle(CaseDessionSaved DecisionData)
        {
            if (DecisionData.DecisionLevel == DecisionLevels.Final)
            {
                CaseComp.UpdateCaseStatus(CaseStatuses.FinalDecision, DecisionData.CaseID);
            }

            if (DecisionData.DecisionLevel == DecisionLevels.Post)
            {
                CaseComp.UpdateCaseStatus(CaseStatuses.PostDecision, DecisionData.CaseID);

            }
            if (DecisionData.DecisionLevel == DecisionLevels.Decision && DecisionData.DecisionType == DecisionTypes.L3_Postponed)
            {
                if (DecisionData.ReservedForJudgement != null)
                {
                    if (DecisionData.ReservedForJudgement.Value)
                    { CaseComp.UpdateCaseStatus(CaseStatuses.ReadyForFinalDecision, DecisionData.CaseID); }

                    else if (!DecisionData.ReservedForJudgement.Value)
                    { CaseComp.UpdateCaseStatus(CaseStatuses.InPrgoress, DecisionData.CaseID); }
                }


            }
        }
        }
    }

