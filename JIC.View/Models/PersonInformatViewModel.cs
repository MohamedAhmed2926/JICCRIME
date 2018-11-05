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
    public class PersonInformatViewModel
    {

        [Display(Name = "CaseTitle", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string CaseName { get; set; }
        [Display(Name = "OverAllNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string OverallNumber { get; set; }
        [Display(Name = "Status", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string Status { get; set; }
        [Display(Name = "CircuitName", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string CircuitName { get; set; }
        [Display(Name = "UserType", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string UserTypes { get; set; }
        [Display(Name = "City", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string Cities { get; set; }
        [Display(Name = "PoliceStation", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string PoliceStations { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string PhoneNo { get; set; }
        [Display(Name = "Nationality", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string Nationalities { get; set; }
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression(@"(2|3)[0-9][0-9][0-1][0-9][0-3][0-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "NationalIdNotValid")]
        [Display(Name = "NationalId", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string NatNo { get; set; }
        [Display(Name = "Name", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression(@"^[\u0621-\u064A\040]+$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InvalidCharacterField")]

        public string Name { get; set; }

  [Display(Name = "NationalId", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string NationalNo { get; set; }
        [Display(Name = "Name", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string PersonName { get; set; }


        [Display(Name = "Job", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string Job { get; set; }
        [Display(Name = "PassportNo", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string PassportNo { get; set; }
        [Display(Name = "Address", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string address { get; set; }
        [Display(Name = "Birthday", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string BirthDate { get; set; }
        [Display(Name = "Age", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string Age { get; set; }
     
        public List<vw_casesViewModel> CasesList = new List<vw_casesViewModel>();
    }

    public class vw_casesViewModel
    {
        [Display(Name = "CaseTitle", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string CaseName { get; set; }
        [Display(Name = "OverAllNumber", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string OverallNumber { get; set; }
        [Display(Name = "Status", ResourceType = typeof(JIC.Base.Resources.Resources))]

        public string Status { get; set; }
    }



}