using JIC.Base.Resources;
using JIC.Base.Views;
using JIC.Crime.View.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CaseBasicDataViewModel
    {
        public CaseBasicDataViewModel()
        {
            CourtID = 0;
            Courts = new List<vw_KeyValue>();
            SecondLevelProsecutions = new List<vw_KeyValue>();
            PoliceStations = new List<vw_KeyValue>();
            FirstLevelProsecutions = new List<vw_KeyValue>();
            AllMainCrimeType = new List<vw_KeyValue>();
        }
        public int UserType { get; set; }
        public int Number { get; set; }
        [Display(Name = "MainCrimeTypeCase", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string MainCrime { get; set; }

        [Display(Name = "MainCrimeTypeCase", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public int? MainCrimeID { get; set; }
        public bool IsSelected { get; set; }

        [Display(Name = "PoliceStation", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string PoliceStationName { get; set; }
        public int CaseID { get; set; }
        public int? OverAllId { get; set; }
        [Display(Name = "CourtName", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string CourtName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Court", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int CourtID { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]

        [Display(Name = "FirstLevelProsecution", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? FirstLevelProsecutionID { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "FirstLevelNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidNumberField")]

        public int? FirstNumber { get; set; }
        public bool IsComplete { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "FirstLevelYear", ResourceType = typeof(Resources))]
        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InValidYearFormat")]
        [ReqiredIfCase]
        public int? FirstYear { get; set; }

        [Display(Name = "PoliceStation", ResourceType = typeof(Resources))]

        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        public int? PoliceStationID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidNumberField")]
        [Display(Name = "SecondLevelNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecondNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
         ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InValidYearFormat")]

        [Display(Name = "SecondLevelYear", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [ReqiredIfCase]
        public int? SecondYear { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
          ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "SecondLevelProsecution", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecondLevelProcID { get; set; }

        public string OrderOfAssignment { get; set; }

        [Display(Name = "CaseTitle", ResourceType = typeof(JIC.Base.Resources.Resources))]
        //[RegularExpression(@"^[\u0621-\u064A\u0660-\u0669 ]+$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "FieldArabicCharcNumberOnly")]
        //[RegularExpression(@"[\u0621-\u064A]|[0-9]", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "FieldArabicCharcNumberOnly")]
        //[RegularExpression(@"^[\u0621-\u064A ][\u0621-\u064A0-9+ ]+", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InvalidCharacterField")]
        
        [RegularExpression(@"^[\u0621-\u064A\0-9\(~!@#4567890_+/*|\}{><.)]+$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InvalidCharacterField")]

        //[RegularExpression(@"([\u0621-\u064A\u0660-\u0669 ]$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidNumberField")]
        public string CaseName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "CrimeType", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int CrimeID { get; set; }
        [Display(Name = "CrimeType", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string CrimeName { get; set; }
        [Display(Name = "OverAllNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public long? OverAllNumber { get; set; }

        [Display(Name = "OverAllNumberProsecution", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? OverAllNumberProsecution { get; set; }

        [Display(Name = "OverAllNumberYear", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? OverAllNumberYear { get; set; }

        [Display(Name = "Obtainments", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public bool HasObtainment { get; set; }
        [Display(Name = "Obtainments", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string Obtainment { get; set; }
        public List<vw_KeyValue> Courts { get; set; }

        [Display(Name = "CaseNationalId", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string CaseNationalID { get; set; }

        public string AllSecondNumber { get; set; }
        public string AllFirstNumber { get; set; }
        public int CrimeNumber { get; set; }
        public string Status { get; set; }
        public string CaseType { get; set; }

        public List<vw_KeyValue> SecondLevelProsecutions { get; set; }
        public List<vw_KeyValue> FirstLevelProsecutions { get; set; }
        public List<vw_KeyValue> PoliceStations { get; set; }
        public List<vw_KeyValue> CrimesTypes { get; set; }
        public List<vw_KeyValue> AllMainCrimeType { get; set; }
        public string FirstprosecutionName { get; set; }
        public string SecoundProsecutionName { get; set; }



    }
}