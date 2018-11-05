using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class RequiredIfCourtUser : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var User = (vw_UserDataModel)validationContext.ObjectInstance;
            if (User.UserType.HasValue && User.UserType.Value != Base.SystemUserTypes.JICAdmin)
                if (!User.CourtID.HasValue)
                    return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
            return ValidationResult.Success;
        }
    }
}