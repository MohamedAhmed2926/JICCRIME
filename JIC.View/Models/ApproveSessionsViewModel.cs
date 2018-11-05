using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class ApproveSessionsViewModel
    {
        public int Order { get; set; }
        public int CaseID { get; set; }
        public string OverAllNumber { get; set; }
        public string FirstLevelNumber { get; set; }
        public string SecondLevelNumber { get; set; }
        public string CaseStatus { get; set; }
        public string MainCrime { get; set; }
        public int? SecretaryID { get; set; }
        
        public long SessionID { get; set; }
        public string Title { get; set; }
        [Display(Name = "SessionDate", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int RollID { get; set; }
        public int CircuitID { get; set; }
        public string RollDate { get; set; }

        public List<vw_KeyValueDate> Sessions { get; set; }
        public List<vw_RollCases> CasesInSession { get; set; }

    }
}