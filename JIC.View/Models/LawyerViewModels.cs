using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class LawyerViewModels
    {
        public int ID { get; set; }
        [Display(Name = "LawyerLevel", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public int LawyerLevelID { get; set; }

        [Display(Name = "lawyerName", ResourceType = typeof(Base.Resources.Resources))]

        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression(@"^[\u0621-\u064A\040]+$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InvalidCharacterField")]

        public string LawyerName { get; set; }

        [Display(Name = "NationalId", ResourceType = typeof(Base.Resources.Resources))]
        [RegularExpression(@"(2|3)[0-9][0-9][0-1][0-9][0-3][0-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "NationalIdNotValid")]

        public string  NationalID { get; set; }

        public long PersonID { get; set; }

        public string LawyerLevelName { get; set; }

        [Display(Name = "LawyerCardNumber", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string LawyerCardNumber { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Base.Resources.Resources))]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Base.Resources.Resources))]
        public string Address { get; set; }
        [Display(Name = "LawyerFileData", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public HttpPostedFileBase LawyerFile { get; set; }

        
        public byte [] LawyerFileData { get; set; }

    }
    public class LawyerCreateViewModel
    {
        public LawyerViewModels LawyerModel { get; set; }
      public  List<LawyerViewModels> Lawyers { get; set; }
        public List<vw_KeyValue> ListLawyerLevelModel { get; set; }

    }
}