using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class RequiredIfJudge : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var User = (vw_UserDataModel)validationContext.ObjectInstance;
            if (User.UserType.HasValue)
                switch (User.UserType.Value)
                {
                    case Base.SystemUserTypes.Judge:
                    case Base.SystemUserTypes.CourtHead:
                        if (!User.UserJudgeLevel.HasValue)
                            return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                        break;
                }
            return ValidationResult.Success;
        }
    }
}