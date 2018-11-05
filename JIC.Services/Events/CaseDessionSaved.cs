using JIC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.EventHandler
{
    public class CaseDessionSaved
    {
        public DecisionLevels DecisionLevel { get; set; } 
        public DecisionTypes DecisionType { get; set; }
        public int CaseID { get; set; }
        public bool? ReservedForJudgement { get; set; }
    }
}
