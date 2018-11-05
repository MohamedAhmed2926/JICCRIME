using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
   public class vw_DefendantsDecisionData
    {
     
        public long CaseDefendantId { get; set; }

        public long SessionDessionId { get; set; }

        public bool IsGuilty { get; set; }

        public string PunishmentDetails { get; set; }

        public int? RestrictionNo { get; set; }

        public int? RestrictionYear { get; set; }

        public int? PunishmentType { get; set; }
        public int DecisionType { get; set; }
    }
}
