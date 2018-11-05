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
    public class SessionsSearchViewModel
    {
        public SessionsSearchViewModel()
        {
            cases = new List<SessionsSearchGridViewModel>();
            FirstLevelProsecutions = new List<vw_KeyValue>();
            SecondLevelProsecutions = new List<vw_KeyValue>();
            Circuits = new List<vw_KeyValue>();
            PoliceStations = new List<vw_KeyValue>();
            DefendantsStautes = new List<vw_KeyValue>();
            MainCrimes = new List<vw_KeyValue>();
            Crimes = new List<vw_KeyValue>();
            CaseStatues = new List<vw_KeyValue>();
            Sessions = new List<vw_KeyValueLongID>();
        }
        
            [Display(Name = "SessionDate", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SessionID { get; set; }
        public string tabName { get; set; }
        public int? CourtID { get; set; }
        [Display(Name = "FirstLevelNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidNumberField")]
        public int? FirstLevelNumber { get; set; }
        [Display(Name = "FirstLevelYear", ResourceType = typeof(Resources))]
        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InValidYearFormat")]
        [Date]
        public int? FirstLevelYear { get; set; }
        [Display(Name = "PoliceStation", ResourceType = typeof(Resources))]

        public int? PoliceStationID { get; set; }
        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidNumberField")]

        [Display(Name = "SecondLevelNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecondLevelNumber { get; set; }

        [RegularExpression(@"[0-9]*$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InValidYearFormat")]
        [Display(Name = "SecondLevelYear", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Date]
        public int? SecondLevelYear { get; set; }

        [Display(Name = "SecondLevelProsecution", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecondLevelProsecutionID { get; set; }
        [Display(Name = "FirstLevelProsecution", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public int? FirstLevelProsecutionID { get; set; }

        [Display(Name = "Circuit", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CircuitID { get; set; }

        [Display(Name = "CrimeType", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CrimeType { get; set; } // np3 el genaya
        [Display(Name = "MainCrime", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public int? CrimeID { get; set; } // el tohma el ra2esya

        [Display(Name = "CaseStatus", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public int? CaseStatusID { get; set; } // no3 el 2deya

        [Display(Name = "DefectsStatus", ResourceType = typeof(JIC.Base.Resources.Resources))]

       

        public int? DefendantStatusID { get; set; }
        public List<vw_KeyValue> FirstLevelProsecutions { get; set; }
        public List<vw_KeyValue> SecondLevelProsecutions { get; set; }
        public List<vw_KeyValue> Circuits { get; set; }
        public List<vw_KeyValue> PoliceStations { get; set; }
        public List<vw_KeyValue> DefendantsStautes { get; set; }
        public List<vw_KeyValue> MainCrimes { get; set; }
        public List<vw_KeyValue> Crimes { get; set; }
        public List<vw_KeyValue> CaseStatues { get; set; }
        public List<SessionsSearchGridViewModel> cases { get; set; }
        public List<vw_KeyValueLongID> Sessions { get; set; }

    }
}