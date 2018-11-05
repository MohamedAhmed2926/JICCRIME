using JIC.Base.Resources;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Models
{
    public class RollOrderCaseViewModel
    {
        [Display(Name = "number", ResourceType = typeof(JIC.Base.Resources.Resources))]
         
        public int Order { get; set; }
        public int CaseID { get; set; }
        [Display(Name = "OverAllNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string OverAllNumber { get; set; }
        [Display(Name = "FirstLevelNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string FirstLevelNumber { get; set; }
        //   public int FirstLevelYear { get; set; }
        //   public string FirstLevelProsecution { get; set; }
        [Display(Name = "SecondLevelNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string SecondLevelNumber { get; set; }
        // public int SecondLevelYear { get; set; }
        //  public string SecondLevelProsecution { get; set; }
        [Display(Name = "CaseStatus", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string CaseStatus { get; set; }
        [Display(Name = "MainCrime", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string MainCrime { get; set; }

        public int? SecretaryID { get; set; }
        
       [Display(Name = "ChooseSession", ResourceType = typeof(JIC.Base.Resources.Resources))]
       [Required(ErrorMessageResourceType = typeof(Messages),ErrorMessageResourceName = "RequiredErrorMessage")]
        public int RollID { get; set; }
         [Display(Name = "Circuit", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public int CircuitID { get; set; }
        public bool ChechOrder { get; set; }
        public List<SelectListItem> UserSecritary { get; internal set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
         ErrorMessageResourceName = "RequiredErrorMessage")]
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

        public int? CurentUserID { get; set; }

        public int? CourtID { get; set; }
    }
    public class RollOrderViewModel
    {
        public RollOrderViewModel()
        {
           
            GetRollsReadyToOrder = new List<vw_KeyValueDate>();
            ListRollToOrder = new List<RollOrderCaseViewModel>();
            Circuits = new List<vw_KeyValue>();
            Roll = new RollOrderCaseViewModel();
            ListUnableOrder = new List<RollOrderCaseViewModel>();
            CaseOrder = new List<CaseOrderViewModel>();
            CircuitMember = new List<vw_CircuitsJudges>();
        }
        public RollOrderCaseViewModel Roll { get; set; }
     
        public  List<vw_KeyValueDate> GetRollsReadyToOrder { get; set; }
        public List<RollOrderCaseViewModel> ListRollBeforeOrder { get; set; }
        public List<RollOrderCaseViewModel> ListRollToOrder { get; set; }
        public List<RollOrderCaseViewModel> ListUnableOrder { get; set; }
        public List<CaseOrderViewModel> CaseOrder { get; set; }
        public List<vw_KeyValue> Circuits { get; set; }
        public List<vw_CircuitsJudges> CircuitMember { get; set; }

    }
 
    }