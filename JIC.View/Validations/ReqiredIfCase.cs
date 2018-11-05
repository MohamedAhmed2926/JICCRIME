using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class ReqiredIfCase : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (Convert.ToInt32(value) > DateTime.Now.Year)
                    return new ValidationResult(JIC.Base.Resources.Messages.InValidFutureYear);
                if ((value.ToString().Length > 4 || value.ToString().Length < 4) || (Convert.ToInt32(value) < 1990))
                    return new ValidationResult(JIC.Base.Resources.Messages.InValidYearFormat);
                else
                    return ValidationResult.Success;
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}