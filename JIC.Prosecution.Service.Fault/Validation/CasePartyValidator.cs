using JIC.Base.Views.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using JIC.Base;
using JIC.Services.ServicesInterfaces;

namespace JIC.Prosecution.Service.Fault.Validation
{
    public class CasePartyValidator : ValidatorBase<CaseParty>
    {
        private readonly ILookupService lookupService;
        public CasePartyValidator(ILookupService lookupService)
        {
            this.lookupService = lookupService;
            RuleFor(CaseParty => CaseParty.ID)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmpyCasePartyID.ToString());
            RuleFor(CaseParty => CaseParty.LegalPerson)
                .NotEmpty()
                .When(CaseParty => CaseParty.IsLegalPerson)
                .WithErrorCode(ErrorCode.EmptyLegalPerson.ToString());
            RuleFor(CaseParty => CaseParty.Order)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.EmptyOrderID.ToString());
            RuleFor(CaseParty => CaseParty.PartyType)
                .Must(ValidatePartyType)
                .WithErrorCode(ErrorCode.InvalidPartyType.ToString());
            RuleFor(CaseParty => CaseParty.DefendantCharges)
                .NotEmpty()
                .When(IsDefendant())
                .WithErrorCode(ErrorCode.EmptyDefendantCharges.ToString());
            RuleFor(CaseParty => CaseParty.DefendantPoliceStationStatusID)
                .NotEmpty()
                .When(IsDefendant())
                .WithErrorCode(ErrorCode.EmptyPoliceStationStatusID.ToString())
                .DependentRules(()=>
                {
                    RuleFor(CaseParty => CaseParty.DefendantPoliceStationStatusID)
                    .Must(ValidateDefendantPoliceStationStatusID)
                    .When(IsDefendant())
                    .WithErrorCode(ErrorCode.InvalidPoliceStationStatusID.ToString());
                });
            RuleFor(CaseParty => CaseParty.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyCasePartyName.ToString());
            RuleFor(CaseParty => CaseParty.Nationality)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyNationalityID.ToString());
        }

        private bool ValidateDefendantPoliceStationStatusID(int? DefenadntStatusID)
        {
            return this.lookupService.GetLookupsByCategory(LookupsCategories.PoliceStationDefendantsStatuses).Exists(DefendantStatus => DefendantStatus.ID == DefenadntStatusID);
        }

        private static Func<CaseParty, bool> IsDefendant()
        {
            return CaseParty => CaseParty.PartyType == (int)PartyTypes.Defendant || CaseParty.PartyType == (int)PartyTypes.VictimAndDefendant;
        }

        private bool ValidatePartyType(int PartyType)
        {
            return PartyType == (int)PartyTypes.Defendant || PartyType == (int)PartyTypes.VictimAndDefendant || PartyType == (int)PartyTypes.Victim;
        }
    }
}