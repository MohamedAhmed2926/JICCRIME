using FluentValidation;
using JIC.Base;
using JIC.Base.Views.Services;
using JIC.Services.ServicesInterfaces;

namespace JIC.Prosecution.Service.Fault.Validation
{
    public class CaseBaseValidator : ValidatorBase<CaseBase>
    {
        private readonly ILookupService LookupService;
        private readonly ICircuitService CircuitService;
        private readonly ICaseConfiguration CaseConfiguration;
        public CaseBaseValidator(
            CasePartyValidator casePartyValidator,
            CaseDescriptionValidator caseDescriptionValidator,
            ILookupService LookupService,
            ICircuitService CircuitService,
            ICaseConfiguration CaseConfiguration)
        {
            this.LookupService = LookupService;
            this.CircuitService = CircuitService;
            this.CaseConfiguration = CaseConfiguration;
            #region CaseBasicData Validator
            RuleFor(Case => Case.Business_Case_Id)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyProsCaseID.ToString());
            RuleFor(Case => Case.Court_ID)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyCourtID.ToString())
                .DependentRules(() =>
                {
                    RuleFor(Case => Case.Court_ID)
                        .Must(CheckCourtIDExist)
                        .WithErrorCode(ErrorCode.InvalidCourtID.ToString());
                });
            RuleFor(Case => Case.CrimeID)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyCrimeID.ToString())
                .DependentRules(() =>
                {
                    RuleFor(Case => Case.CrimeID)
                        .Must(CheckCrimeIDExist)
                        .WithErrorCode(ErrorCode.InvalidCrimeID.ToString());
                });
            RuleFor(Case => Case.ProcedureTypeID)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyProcedureTypeID.ToString())
                .DependentRules(() =>
                {
                    RuleFor(Case => Case.ProcedureTypeID)
                        .Must(CheckProcedureType)
                        .WithErrorCode(ErrorCode.InvalidProcedureTypeID.ToString());
                });
            RuleFor(Case => Case.CaseTypeID)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyCaseTypeID.ToString())
                .DependentRules(() =>
                {
                    RuleFor(Case => Case.CaseTypeID)
                        .Must(CheckCaseTypeID)
                        .WithErrorCode(ErrorCode.InvalidCaseTypeID.ToString());
                });
            RuleFor(Case => Case.First_Case_No)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyFirstCaseNo.ToString());
            RuleFor(Case => Case.First_Case_Police_Station_ID)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyFirstPoliceStationID.ToString())
                .DependentRules(() =>
                 {
                     RuleFor(Case => Case.First_Case_Police_Station_ID)
                    .Must(CheckPoliceStationIDExist)
                    .WithErrorCode(ErrorCode.InvalidFirstPoliceStationID.ToString());
                 });
            RuleFor(Case => Case.First_Case_Year)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyFirstCaseYearID.ToString());
            #endregion

            #region CaseDescriptionValidation
            RuleFor(Case => Case.CaseDescription)
                .NotNull()
                .WithErrorCode(ErrorCode.NullCaseDescription.ToString())
                .DependentRules(() =>
                {
                    RuleFor(Case => Case.CaseDescription).SetValidator(caseDescriptionValidator);
                });

            #endregion

            #region CaseParties
            RuleFor(Case => Case.CaseParties)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyCaseParties.ToString())
                .DependentRules(() =>
                {
                    RuleFor(Case => Case.CaseParties)
                        .SetCollectionValidator(casePartyValidator);


                    //Check For If Defendant and Victim Exist
                    RuleFor(Case => Case.CaseParties)
                        .Must(CaseParties => CaseParties.Exists(CaseParty => (CaseParty.PartyType == (int)PartyTypes.Victim || CaseParty.PartyType == (int)PartyTypes.VictimAndDefendant)))
                        .WithErrorCode(ErrorCode.VictimDoesntExist.ToString());

                    RuleFor(Case => Case.CaseParties)
                        .Must(CaseParties => CaseParties.Exists(CaseParty => (CaseParty.PartyType == (int)PartyTypes.Defendant || CaseParty.PartyType == (int)PartyTypes.VictimAndDefendant)))
                        .WithErrorCode(ErrorCode.DefendantDoesntExist.ToString());

                    RuleFor(Case => Case.CaseParties)
                        .Must(CaseParties => CaseParties.Count > 1)
                        .When(Case => Case.CaseParties.Exists(CaseParty => CaseParty.PartyType == (int)PartyTypes.VictimAndDefendant))
                        .WithErrorCode(ErrorCode.OnlyOneVictimDefendantExist.ToString());
                });

            #endregion
        }

        private bool CheckCaseTypeID(int CaseTypeID)
        {
            return this.LookupService.GetCaseTypes().Exists(CaseType => CaseType.ID == CaseTypeID);
        }

        private bool CheckPoliceStationIDExist(int PoliceStationID)
        {
            return this.LookupService.GetAllPoliceStations().Exists(PoliceStation => PoliceStation.ID == PoliceStationID);
        }

        private bool CheckCourtIDExist(int CourtID)
        {
            return this.LookupService.GetCourts().Exists(Court => Court.ID == CourtID);
        }

        private bool CheckCrimeIDExist(int CrimeID)
        {
            return this.LookupService.GetLookupsByCategory(JIC.Base.LookupsCategories.Crimes).Exists(Crime => Crime.ID == CrimeID);
        }
        private bool CheckProcedureType(int ProcedureTypeID)
        {
            return ProcedureTypeID == (int)JIC.Base.CaseProcedureTypes.Case || ProcedureTypeID == (int)JIC.Base.CaseProcedureTypes.ProsecutionJudgment;
        }
    }
}