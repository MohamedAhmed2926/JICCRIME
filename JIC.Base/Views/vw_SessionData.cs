using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_SessionData
    {
        public int SessionID { get; set; }
        public DateTime SessionDate { get; set; }
        public string DayName { get; set; }

        public int ID { get; set; }

        public int CaseID { get; set; }

        public long RollID { get; set; }

        public bool DoneByDefaultCircuit { get; set; }

        public int RollIndex { get; set; }

        public int? ProsecuterID { get; set; }
        public int? HallID { get; set; }
        public string MunitesOfSession { get; set; }

        public bool ApprovedByJudge { get; set; }

        public bool? IsTransferedFromSession { get; set; }

        public bool? IsPendingOnTransfer { get; set; }

        public bool? IsTransferedApproved { get; set; }

        public string CircuitName { get; set; }
        public string ProsecutorName { get; set; }
        public string SecretaryAssistantName { get; set; }

    }
}
