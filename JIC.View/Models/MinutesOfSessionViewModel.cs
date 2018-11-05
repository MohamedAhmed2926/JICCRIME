using JIC.Base.Resources;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace JIC.Crime.View.Models
{
    public class MinutesOfSessionViewModel
    {

        public int CaseID { get; set; }
        public int SessionID { get; set; }
        public int CrimeTypeID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string Text { get; set; }
        public int CircuitID { get; set; }
       
        [Display(Name = "HeadJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string HeadJudge { get; set; }

        [Display(Name = "FirstJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string FirstJudge { get; set; }

        [Display(Name = "SecondJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string SecondJudge { get; set; }


        [Display(Name = "ThirdJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string ThirdJudge { get; set; }


        [Display(Name = "FourthJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string FourthJudge { get; set; }


        [Display(Name = "FifthJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string FifthJudge { get; set; }


        [Display(Name = "SixthJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string SixthJudge
        {
            get; set;


        }

        [Display(Name = "alternativeJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string alternativeJudge { get; set; }

        [Display(Name = "JudgeCount", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? JudgeCount { get; set; }
       public bool SaveAttendance { get; set; }
        public bool SaveMinutes { get; set; }
        public bool SavedBefore { get; set; }

        public bool SavedDecisions { get; set; }
        public bool SentToJudge { get;  set; }
        public int? CurentUserID { get; set; }

        public int? CourtID { get; set; }
        public long RollID { get; set; }

        //   public List<ListOfDefendantViewModel> ListOfDefendantModel { get; set; }
    }

    public class MinutesOfSessionCreateViewModel
    {
        public MinutesOfSessionViewModel MinutesOfSession { get; set; }
        public List<CaseDefectsDataViewModel> CaseDefectsData { get; set; }
        public List<CaseDefectsDataViewModel> CaseVictims { get; set; }
        public int CrimeType { get; set; }
        public vw_SessionData SessionData { get; set; }
        public vw_CrimeCaseBasicData CaseBasicData { get; set; }
      
        public IDictionary<string, string> AutoCompleteText { get; set; }
        public List<vw_CircuitsJudges> vw_CircuitsGrid { get; set; }
        public CircuitConfigurationViewModel circuitConfigurationViewModel { get; set; }
        public string GetAutoCompleteJS()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(AutoCompleteText);
        }
    }
}