using JIC.Base.Resources;
using JIC.Base.Views;
using JIC.Crime.View.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CircuitConfigurationViewModel
    {

        public GetCircuitData getCircuit { get; set; }
        public CircuitConfigurationViewModel()
        {
            AllSecretaries = new List<vw_KeyValue>();
            PoliceStation = new int[0];
            AllPoliceStation = new List<vw_KeyValue>();
            AllCourts = new List<vw_KeyValue>();
            JudgeCounts = new List<vw_KeyValue>()
            {
                new vw_KeyValue
                {
                    ID=2,
                    Name="2"
                },
                new vw_KeyValue
                {
                    ID=4,
                    Name="4"
                },
                new vw_KeyValue
                {
                    ID=6,
                    Name="6"
                }
            };
        }
      
        [Display(Name = "CourtName", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string CourtName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Messages),
        //   ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Circuit", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CircuitID { get; set; }


        [Display(Name = "Court", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CourtID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Circuit", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RegularExpression(@"^[\u0621-\u064A ][\u0621-\u064A0-9+ ]+", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InvalidCharacterField")]
        //[ArabicAndNumber(ErrorMessage = "Please use a valid email address")]
        public string CircuitName { get; set; }

        [Display(Name = "CircuitStartDate", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        //[RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InValidDate")]

        //[RegularExpression(@"[0-3][0-9]\/[0-1] [0-9]\/[1-2][0-9][0-9][0-9]", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InValidDate")]
        [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])[-/](1[0-2]|0[1-9])[-/][0-9]{4}$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InValidDate")]

        [DateNotLessThanToday]
        [MaxNextYear]
        public string CircuitStartDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Cycles", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? Cycle { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "JudgeCount", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? JudgeCount { get; set; }



        //public int? PoliceStation { get; set; }
        public List<vw_CircuitsJudges> CircuitJudges { get; set; }
        public List<vw_KeyValue> Circuits { get; set; }
        public List<vw_KeyValue> JudgeCounts { get; set; }
        public List<vw_KeyValue> AllJudges { get; set; }
        public List<vw_KeyValue> AllCrimes { get; set; }
        public List<vw_KeyValue> AllPoliceStation { get; set; }
        public List<vw_KeyValue> AllCycles { get; set; }
        public List<vw_KeyValue> AllSecretaries { get; set; }
        public List<vw_KeyValue> AllCourts { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
          ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "HeadJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? HeadJudge { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
          ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "SecretaryHead", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecretaryHead { get; set; }

    
        [Display(Name = "SecretaryAssistant", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecretaryAssistant { get; set; }

        [Display(Name = "CrimeType", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? CrimeType { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
          ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "FirstJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? FirstJudge { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
          ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "SecondJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SecondJudge { get; set; }


        [Display(Name = "ThirdJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? ThirdJudge { get; set; }


        [Display(Name = "FourthJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? FourthJudge { get; set; }


        [Display(Name = "FifthJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? FifthJudge { get; set; }


        [Display(Name = "SixthJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? SixthJudge { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),
      ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "PoliceStation", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [MinLength(1)]
        public int[] PoliceStation { get; set; }

        [Display(Name = "alternativeJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int? alternativeJudge { get; set; }



       

    }
    public class GetCircuitData
    {
        public CircuitConfigurationViewModel _CircuitConfigurationViewModel { get; set; }
        public int? ID { get; set; }
        [Display(Name = "CrimeType", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string CrimeTypeName { get; set; }



        [Display(Name = "PoliceStation", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string PoliceStationsName { get; set; }
        public List<vw_KeyValue> PoliceStation { get; set; }

        [Display(Name = "HeadJudge", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string HeadJugeName { get; set; }

        [Display(Name = "CircuitStartDate", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public DateTime CircuitStartDate { get; set; }
        [Display(Name = "CircuitStartDate", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string StartDate { get; set; }

        //[Display(Name = "PoliceStation", ResourceType = typeof(JIC.Base.Resources.Resources))]
        //public List<vw_KeyValue> PoliceStations { get; set; }

        [Display(Name = "Cycles", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string CycleName { get; set; }

        [Display(Name = "Circuit", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string CircuitName { get; set; }
    }
}