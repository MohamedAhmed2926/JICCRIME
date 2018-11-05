using JIC.Base;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class RequiredIfProsecutionUser : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var User = (vw_UserDataModel)validationContext.ObjectInstance;
            if (User.UserType.HasValue)
                switch (User.UserType.Value)
                {
                    case SystemUserTypes.schedualEmployee:
                    case SystemUserTypes.ImplementationEmployee:
                        if (!User.ProsecutionID.HasValue)
                            return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                        break;
                }
            return ValidationResult.Success;
        }
    }
}