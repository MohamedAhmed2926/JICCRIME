using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public enum CaseSession
    {
        DefectsAttendance,
        Session,
        Decisionsandjudgments,
        Attachments,
        SessionApproval
    }
    public class CaseSessionViewModel
    {
        public int SessionID { get; set; }
        public int id { get; set; }
        public CaseSession CaseSessionMode { get; set; }
    }
}