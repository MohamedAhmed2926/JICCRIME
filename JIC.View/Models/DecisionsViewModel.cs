using JIC.Base;
using JIC.Base.Resources;
using JIC.Base.Views;
using JIC.Crime.View.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Models
{
    public class DecisionsViewModel
    {
        public DecisionsViewModel()
        {
            CaseJudmentType = new List<vw_KeyValue>();
            Sessions = new List<vw_KeyValueLongID>();
            DectionTypes = new List<vw_KeyValue>();
            Judgments = new List<vw_KeyValue>();
        }
        
        public int CaseID { get; set; }

        public string CaseResultType { get; set; }
        public int CaseNumber { get; set; }
        public DateTime SessionDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        public string DecionDescription { get; set; }

        public DateTime DecisionDate { get; set; }

        public long CaseSessionID { get; set; }

        public string DecisionText { get; set; }
        [CaseDecisionType]
        [Display(Name = "DectionType", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public short? DecisionTypeID { get; set; }
        //[PostJudge]
        //[PostDecision]
        [Display(Name = "Session", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? NextSessionDate { get; set; }


        [Display(Name = "CaseStatus_ReadyForFinalDecision", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public bool? IsReadyForFinalDecision { get; set; }

        [Display(Name = "Judgment", ResourceType = typeof(JIC.Base.Resources.Resources))]
        //[PostJudge]
        public int? JudgmentID { get; set; }
        public List<vw_KeyValue> Judgments { get; set; }
        public List<CaseDefentantsViewModel> DefendantsList { get; set; }
        public List<vw_KeyValue> CaseJudmentType { get; set; }
        [Display(Name = "JudgmentType", ResourceType = typeof(JIC.Base.Resources.Resources))]
        //[CaseJudgmentType]
        public int? CaseJudmentTypeID { get; set; }


        public List<vw_KeyValueLongID> Sessions { get; set; }
        public List<vw_KeyValue> DectionTypes { get; set; }


    }
}