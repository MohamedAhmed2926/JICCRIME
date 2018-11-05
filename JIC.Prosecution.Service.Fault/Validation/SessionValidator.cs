using FluentValidation;
using JIC.Base.Views.Services;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Prosecution.Service.Fault.Validation
{
    public class SessionValidator : ValidatorBase<CaseSession>
    {
        private readonly ICircuitService CircuitService;
        private readonly ICaseConfiguration CaseConfiguration;
        public SessionValidator(
            ICircuitService CircuitService,
            ICaseConfiguration CaseConfiguration)
        {
            this.CircuitService = CircuitService;
            this.CaseConfiguration = CaseConfiguration;
            RuleFor(Session => Session.Circuit_ID)
                .NotEmpty()
                .WithErrorCode(ErrorCode.EmptyCircuitID.ToString());
            RuleFor(Session => Session.Circuit_ID)
                    .Must(CheckCircuitIDExist)
                    .WithErrorCode(ErrorCode.InvalidCircuitID.ToString());
            RuleFor(Session => Session.Session_Date)
                    .NotEmpty()
                    .WithErrorCode(ErrorCode.EmptySessionDate.ToString());
            RuleFor(Session => Session.Session_Date)
                    .Must(CheckFirstSessionExist)
                    .WithErrorCode(ErrorCode.InvalidSessionDate.ToString());
            RuleFor(Session => Session.Reservation_Code)
                    .NotEmpty()
                    .WithErrorCode(ErrorCode.EmptyReservationCode.ToString());
        }
        public bool CheckFirstSessionExist(CaseSession Session, DateTime FirstSessionDate)
        {
            return CaseConfiguration.GetCircuitRolls(Session.Circuit_ID,Session.CaseTypeID).Where(CircuitData => CircuitData.Date == FirstSessionDate).Count() > 0;
        }
        public bool CheckCircuitIDExist(CaseSession Session, int CircuitID)
        {
            return this.CircuitService.GetCircuitsByCourtID(Session.Court_ID).Exists(Circuit => Circuit.ID == CircuitID);
        }

    }
}