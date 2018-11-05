using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Prosecution.Service.Fault.Validation
{
    public abstract class ValidatorBase<T> : AbstractValidator<T>
    {
        public bool CValidate(T type, out List<int> ErrorCodes)
        {
            var Validation = Validate(type);
            if (Validation.IsValid)
            {
                ErrorCodes = null;
                return true;
            }
            else
            {
                ErrorCodes = Validation.Errors.Select(error => int.Parse(error.ErrorCode)).ToList();
                return false;
            }
        }
    }
}