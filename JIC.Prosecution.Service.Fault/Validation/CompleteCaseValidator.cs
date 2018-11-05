using FluentValidation;
using JIC.Base.Views.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Prosecution.Service.Fault.Validation
{
    public class CompleteCaseValidator : ValidatorBase<CompleteCase>
    {
        public CompleteCaseValidator(
            CaseBaseValidator caseBaseValidator,
            SessionValidator finalSessionValidator)
        {
            #region CaseBasicData Validation
            RuleFor(Case => Case)
                .SetValidator(caseBaseValidator);
            #endregion

            #region Case Session Validation
            RuleFor(Case => Case.FinalSession)
                .NotNull()
                .WithErrorCode(ErrorCode.NullSession.ToString())
                .DependentRules(() =>
                {
                    RuleFor(Case => Case.FinalSession).SetValidator(finalSessionValidator);
                });
            #endregion
        }
    }
}