using JIC.Base.Resources;
using JIC.Base.Views;
using JIC.Crime.View.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Models
{
    public class WitnessViewModel
    {
        public int CaseID { get; set; }

        [Display(Name = "Age", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int Age { get; set; }
        public long ID { get; set; } = 0;
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Name", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [MaxLength(250, ErrorMessage = "إسم الشخص لا يمكن ان يتخطى 250 حرف")]

        [RegularExpression("^([\u0621-\u064A ]+[^ ]+$)", ErrorMessage = "الإسم غير صحيح")]
        public virtual string Name { get; set; }
        [Display(Name = "NationalId", ResourceType = typeof(JIC.Base.Resources.Resources))]
        //[StringLength(14, MinimumLength = 14, ErrorMessage = "الرقم القومى غير صحيح")]
        ////[RegularExpression("^[0-9]*", ErrorMessage = "الرقم القومى مكون من أرقام فقط")]
        //[Unique(UniqueTypes.NationalNo)]
        [RegularExpression(@"(2|3)[0-9][0-9][0-1][0-9][0-3][0-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "NationalIdNotValid")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public virtual string NatNo { get; set; }
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Nationality", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public virtual string NationalityName { get; set; } 
        public bool NationalIDRequired { get; set; } = false;

        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Nationality", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public virtual int? NationalityID { get; set; } = (int)Base.Nationality.Egyptian;
        [Display(Name = "PassportNo", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RegularExpression("^([a-zA-z0-9*-@#$!#&_/]+[^ ]+)", ErrorMessage = "رقم جواز السفر غير صحيح")]
        // [Unique(UniqueTypes.PassportNo)]

        public virtual string PassportNo { get; set; }
        [Display(Name = "Job", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression("^([\u0621-\u064A ]+[^ ]+)", ErrorMessage = "يجب ان يكون هذا الحقل باللغة العربية")]
        public virtual string Job { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        [Display(Name = "City", ResourceType = typeof(Base.Resources.Resources))]
        public virtual int? address_CityID { get; set; }
        [Display(Name = "PoliceStation", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public virtual int? address_PoliceStationID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        [Display(Name = "Address", ResourceType = typeof(Base.Resources.Resources))]
        public virtual string address_address { get; set; }
        [Display(Name = "Birthday", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [StrDateBeforeToday]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])[-/](1[0-2]|0[1-9])[-/][0-9]{4}$", ErrorMessage = "تاريخ الميلاد غير صحيح")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public virtual string BirthDate { get; set; }
        public DateTime? BirthDateT { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public virtual string PhoneNo { get; set; }

        public bool PhoneNoRequired { get; set; } = false;

        public DateTime? GetBirthDate()
        {
            DateTime output = DateTime.Now;
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (DateTime.TryParseExact(BirthDate, JIC.Base.SystemConfigurations.DateTime_ShortDateFormat, provider, DateTimeStyles.None, out output))
            {
                return output;
            }
            return null;
            //return BirthDate;
        }



        public List<SelectListItem> AtendanceWitnessType { get; set; }


        // public vw_PersonData PersonData { get; set; }
        public List<vw_KeyValue> Cities { get; set; }
        public List<vw_KeyValue> PoliceStations { get; set; }
        public List<vw_KeyValue> Nationalities { get; set; }
        public bool IsLocked { get; internal set; }
    }
}