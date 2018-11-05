using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class GreaterThan : ValidationAttribute
    {
        private int value;
        public GreaterThan(int value)
        {
            this.value = value;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (((int)value) > this.value)
                return ValidationResult.Success;

            return new ValidationResult("لا بد ان يكون الرقم أكبر من " + this.value);
        }
    }
}