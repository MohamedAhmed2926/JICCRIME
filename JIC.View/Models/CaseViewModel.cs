using JIC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CaseViewModel
    {
        public CaseViewModel()
        {
            statusCheck = StatusCheck.OrderOFAssignmentNotExist;
        }
        public int CaseID { get; set; }
        public PageMode PageMode { get; set; }

        public CaseStatus caseStatus { get; set; }

        public StatusCheck statusCheck { get; set; }
    }
}