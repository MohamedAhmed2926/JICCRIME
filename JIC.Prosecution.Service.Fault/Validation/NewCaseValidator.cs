using FluentValidation;
using JIC.Base.Views.Services;

namespace JIC.Prosecution.Service.Fault.Validation
{
    public class NewCaseValidator : ValidatorBase<NewCase>
    {
        public NewCaseValidator(
            CaseBaseValidator caseBaseValidator,
            SessionValidator firstSessionValidator)
        {
            #region CaseBasicData Validation
            RuleFor(Case => Case)
                .SetValidator(caseBaseValidator);
            #endregion

            #region Case Session Validation
            RuleFor(Case => Case.First_Session)
                .NotNull()
                .WithErrorCode(ErrorCode.NullSession.ToString())
                .DependentRules(() =>
                {
                    RuleFor(Case => Case.First_Session).SetValidator(firstSessionValidator);
                });
            #endregion
        }

        
    }
}