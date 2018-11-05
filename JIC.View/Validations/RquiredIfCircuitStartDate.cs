using JIC.Base.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class RquiredIfCircuitStartDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime maxdate = new DateTime(((DateTime.Now.Year) + 1), 12, 31);
            if (Convert.ToDateTime(value) < DateTime.Now)
                return new ValidationResult(Messages.InvalidDateLessThanToday);
            if(Convert.ToDateTime(value) > maxdate)
                return new ValidationResult(Messages.InvalidMaxDate);
            return ValidationResult.Success;
        }
    }
}