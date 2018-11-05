using JIC.Base;
using JIC.Base.Resources;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class OpenRollViewModel
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
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public int RollID { get; set; }
        [Display(Name = "Circuit", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public int CircuitID { get; set; }
        public bool ChechOrder { get; set; }
        [Display(Name = "ProsecuterName", ResourceType = typeof(Base.Resources.Resources))]

        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public int prosecutorID { get; set; }
        [Display(Name = "HallName", ResourceType = typeof(Base.Resources.Resources))]

        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public int HallID { get; set; }

        public MinutesStatus MinutesStatus { get; set; }

        public RollStatus rollStatus { get; set; }
        public long SessionID { get; set; }
        public int? CurentUserID { get; set; }

        public int? CourtID { get; set; }
    }
        public class OpenRollCreateViewModel
    {
        public OpenRollViewModel OPenRoll { get; set; }

        public List<vw_KeyValue> Circuits { get; set; }
        public vw_SessionData SessionData { get; set; }
        public List<OpenRollViewModel> ListRollOpen { get; set; }
        public List<OpenRollViewModel> ListCasesInRoll { get; set; }

        public List<ProsecutorViewModels> Prosecutors { get; set; }
        public List<vw_KeyValue> Halls { get; set; }
        public List<vw_SessionData> AllOpenRolls { get; set; }



    }
}