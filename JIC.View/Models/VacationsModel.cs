using JIC.Base.Resources;
using JIC.Crime.View.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class VacationsModel
    {


        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Name_Vacations", ResourceType = typeof(Base.Resources.Resources))]
        [RegularExpression(@"^[\u0621-\u064A ][\u0621-\u064A0-9+ ]+",ErrorMessage = "يجب أن لا يحتوى اسم الاجازة على حروف خاصة او لغات اخرى")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "FromDate", ResourceType = typeof(Base.Resources.Resources))]
        [DataType(DataType.Date,ErrorMessage = "يجب ان يحتوى هذا الحقل على تاريخ فقط")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        [DateNotLessThanToday]
        public DateTime  FromDate { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "DateFieldError", ErrorMessageResourceType = typeof(Base.Resources.Messages))]
        [Required(AllowEmptyStrings = false,ErrorMessageResourceType = typeof(Messages),ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "EndDate", ResourceType = typeof(Base.Resources.Resources))]
        [DataType(DataType.Date,ErrorMessage ="يجب ان يحتوى هذا الحقل على تاريخ فقط")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        [DateNotLessThanToday]
        public DateTime  EndDate { get; set; }




    }

    
}