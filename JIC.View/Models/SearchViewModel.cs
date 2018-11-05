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
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            SecondLevelProsecutions = new List<vw_KeyValue>();
            FirstLevelProsecutions = new List<vw_KeyValue>();
            PoliceStations = new List<vw_KeyValue>();
            CrimesTypes = new List<vw_KeyValue>();
            PartyTypes = new List<vw_KeyValue>();
            JudgeTypes = new List<vw_KeyValue>();
            Circuits = new List<vw_KeyValue>();
            SessionDateTypes = new List<vw_KeyValue>();

            CaseTypes = new List<vw_KeyValue>();
            MainCrimesType = new List<vw_KeyValue>();
            DefectsStatues = new List<vw_KeyValue>();
            SearchGrid = new List<SearchGridViewModel>();
        }

        [Display(Name = "Court", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CourtID { get; set; }

        [Display(Name = "Court", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string CourtName { get; set; }


        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidNumberField")]
        [Display(Name = "FirstLevelNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? FirstNumber { get; set; }

        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InValidYearFormat")]
        [ReqiredIfCase]
        [Display(Name = "OverAllNumberYear", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? FirstYear { get; set; }
        [Display(Name = "FirstLevelProsecution", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? FirstLevelProsecutionID { get; set; }

        [Display(Name = "Sessions", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SessionDateType { get; set; }

        [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])[-/](1[0-2]|0[1-9])[-/][0-9]{4}$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InValidDate")]
        [Display(Name = "SessionDate", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
      //  [DataType(DataType.Date, ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InValidDate")]

        public string SessionDate { get; set; }

        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidNumberField")]
        [Display(Name = "SecondLevelNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecondNumber { get; set; }

        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InValidYearFormat")]
        [ReqiredIfCase]
        [Display(Name = "OverAllNumberYear", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecondYear { get; set; }

        [Display(Name = "SecondLevelProsecution", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecondLevelProsecutionID { get; set; }



        [Display(Name = "PoliceStation", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? PoliceStationID { get; set; }

        [Display(Name = "JudgeDate", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])[-/](1[0-2]|0[1-9])[-/][0-9]{4}$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InValidDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string JudgeDate { get; set; }

        [Display(Name = "CaseStatus", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? JudgeType { get; set; }

        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidNumberField")]
        [Display(Name = "OverAllNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? OverAllNumber { get; set; }

        [Display(Name = "SecondLevelProsecution", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? OverAllNumberProsecution { get; set; }

        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InValidYearFormat")]
        //[ReqiredIfCase]
        [Display(Name = "OverAllNumberYear", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? OverAllNumberYear { get; set; }

        [Display(Name = "PartyType", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? PartyType { get; set; }

        [Display(Name = "PartyName", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string PartyName { get; set; }

        [Display(Name = "HasObtainment", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? HasObtainment { get; set; }

        [Display(Name = "CrimeType", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CrimeType { get; set; }

        [Display(Name = "Circuit", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CircuitID { get; set; }
        [Display(Name = "MainCrime", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? MainCrimeID { get; set; }
        [Display(Name = "DefectsStatus", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? DefectsStatusID { get; set; }
        [Display(Name = "CaseStatus", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CaseTypeID { get; set; }
        public List<vw_KeyValue> SecondLevelProsecutions { get; set; }
        public List<vw_KeyValue> FirstLevelProsecutions { get; set; }
        public List<vw_KeyValue> PoliceStations { get; set; }
        public List<vw_KeyValue> CrimesTypes { get; set; }
        public List<vw_KeyValue> PartyTypes { get; set; }
        public List<vw_KeyValue> JudgeTypes { get; set; }
        public List<vw_KeyValue> Circuits { get; set; }
        public List<vw_KeyValue> SessionDateTypes { get; set; }

        public List<vw_KeyValue> CaseTypes { get; set; }
        public List<vw_KeyValue> MainCrimesType { get; set; }
        public List<vw_KeyValue> DefectsStatues { get; set; }
        public List<SearchGridViewModel> SearchGrid { get; set; }
        public List<vw_KeyValue> ObtainmentStatuses { get; set; }
        public List<vw_KeyValue> Courts { get; internal set; }
    }
}