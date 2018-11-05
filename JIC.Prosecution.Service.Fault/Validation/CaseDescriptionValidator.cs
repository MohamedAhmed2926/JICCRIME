using FluentValidation;
using JIC.Base.Views.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Prosecution.Service.Fault.Validation
{
    public class CaseDescriptionValidator :ValidatorBase<CaseDescription>
    {
        public CaseDescriptionValidator()
        {
            RuleFor(CaseDescription => CaseDescription.Description)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyCaseDescription.ToString());
            RuleFor(CaseDescription => CaseDescription.LawItems)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyLawItems.ToString());
        }
    }
}