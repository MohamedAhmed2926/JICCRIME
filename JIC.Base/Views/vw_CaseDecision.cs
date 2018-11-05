using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CaseDecision
    {

        public int CaseID { get; set; }

        public DateTime DecisionDate { get; set; }

        public int CaseSessionID { get; set; }

        public string DecisionDescription { get; set; }

        public short DecisionTypeID { get; set; }

        public DateTime? NextSessionDate { get; set; }

        public bool? PaymentStatus { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? OldCircuitID { get; set; }

        public int? NewCircuitID { get; set; }

        public int? CycleRollID { get; set; }

        public int? RollID { get; set; }

        public bool? ReservedForJudgement { get; set; }
        public List<vw_DefendantsDecisionData> DefendantsListJudges { get; set; }       
        public DecisionTypes DecisionType { get; set; }
        public DecisionLevels DecisionLevel { get; set; }
    }
}
