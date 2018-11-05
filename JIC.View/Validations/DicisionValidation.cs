using JIC.Base;
using JIC.Base.Resources;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class DicisionValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Decisions = (DecisionsViewModel)validationContext.ObjectInstance;
            DecreeTypes decreeTypes = (DecreeTypes)Enum.Parse(typeof(DecreeTypes), Decisions.CaseResultType);
            switch (decreeTypes)
            {
                case DecreeTypes.Judgement:
                    if (!Decisions.CaseJudmentTypeID.HasValue)
                        return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                    else
                    {
                        if (Decisions.CaseJudmentTypeID == (int)CaseStatuses.PostDecision)
                        {
                            if (!Decisions.JudgmentID.HasValue || string.IsNullOrEmpty(Decisions.NextSessionDate.ToString()))
                                return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                        }
                    }
                    break;
                case DecreeTypes.Decision:
                    if (!Decisions.DecisionTypeID.HasValue)
                        return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                    else
                    {
                        if (Decisions.DecisionTypeID == (int)DecisionTypes.L3_Postponed)
                        {
                            if (string.IsNullOrEmpty(Decisions.NextSessionDate.ToString()))
                                return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                            if (!Decisions.IsReadyForFinalDecision.HasValue)
                                return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                        }
                    }
                    break;
            }
            return ValidationResult.Success;
        }
    }
    public class CaseDecisionType : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Decisions = (DecisionsViewModel)validationContext.ObjectInstance;
            if (Decisions.CaseResultType != null)
            {
                DecreeTypes decreeTypes = (DecreeTypes)Enum.Parse(typeof(DecreeTypes), Decisions.CaseResultType);
                switch (decreeTypes)
                {
                    case DecreeTypes.Decision:
                        if (!Decisions.DecisionTypeID.HasValue)
                            return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                        break;

                }
            }
            return ValidationResult.Success;
        }
    }
    public class CaseJudgmentType : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Decisions = (DecisionsViewModel)validationContext.ObjectInstance;
            if (Decisions.CaseResultType != null)
            {
                DecreeTypes decreeTypes = (DecreeTypes)Enum.Parse(typeof(DecreeTypes), Decisions.CaseResultType);
                switch (decreeTypes)
                {
                    case DecreeTypes.Judgement:
                        if (!Decisions.CaseJudmentTypeID.HasValue)
                            return new ValidationResult(JIC.Base.Resources.Resources.RequiredErrorMessage);
                        break;

                }
            }
            return ValidationResult.Success;
        }
    }
    public class PostJudge : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Decisions = (DecisionsViewModel)validationContext.ObjectInstance;
           
            if (Convert.ToInt32(Decisions.CaseResultType) == (int)DecreeTypes.Judgement)
            {
                if (!Decisions.JudgmentID.HasValue || string.IsNullOrEmpty(Decisions.NextSessionDate.ToString()))
                    return new ValidationResult(Resources.RequiredErrorMessage);
                if (string.IsNullOrEmpty(Decisions.NextSessionDate.ToString()))
                    return new ValidationResult(Resources.RequiredErrorMessage);
            }
            return ValidationResult.Success;

        }

    }
    public class Decision : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Decisions = (DecisionsViewModel)validationContext.ObjectInstance;
            if (Convert.ToInt32(Decisions.CaseResultType) == (int)DecreeTypes.Decision)
            {
                if (!Decisions.DecisionTypeID.HasValue)
                    return new ValidationResult(Resources.RequiredErrorMessage);
                else
                    return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
    public class PostDecision : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Decisions = (DecisionsViewModel)validationContext.ObjectInstance;
            if (Convert.ToInt32(Decisions.CaseResultType) == (int)DecreeTypes.Decision && Decisions.DecisionTypeID == (short)DecisionTypes.L3_Postponed)
            {
                if (!Decisions.DecisionTypeID.HasValue || string.IsNullOrEmpty(Decisions.NextSessionDate.ToString()))
                    return new ValidationResult(Resources.RequiredErrorMessage);
                else
                    return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}