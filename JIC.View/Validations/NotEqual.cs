using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class NotEqual : ValidationAttribute
    {
        private string Comparator;
        public NotEqual(string Comparator)
        {
            this.Comparator = Comparator;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string OriginalCompare = (string)value;
            if (!OriginalCompare.Equals(Comparator))
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
    public class NotEqualDefaultPassword : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string OriginalCompare = (string)value;
            if (!OriginalCompare.Equals(JIC.Base.SystemConfigurations.Defaults_DefaultPassword))
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}