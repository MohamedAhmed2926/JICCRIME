using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using JIC.Base.Views;
using JIC.Crime.View.Validations;

namespace JIC.Crime.View.Models
{
    public class UserPersonViewModel : vw_PersonData
    {
        public UserPersonViewModel()
        {
        }
        //[Display(Name = "City", ResourceType = typeof(Base.Resources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        //public override int? address_CityID { get; set; }
        //[Display(Name = "PoliceStation", ResourceType = typeof(Base.Resources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        //public override int? address_PoliceStationID { get; set; }
        [Display(Name = "NationalId", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "الرقم القومى غير صحيح")]
        [RegularExpression("^[0-9]*", ErrorMessage = "الرقم القومى مكون من أرقام فقط")]
        [Unique(UniqueTypes.NationalNo)]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        public override string NatNo { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        public override string address_address { get; set; }
    }
}