using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    public class ErrorCode
    {
        public const int Failed = 1;
        public const int EmptyProsCaseID = 2;
        public const int EmptyCourtID = 3;
        public const int EmptyFirstCaseNo = 4;
        public const int EmptyFirstPoliceStationID = 5;
        public const int EmptyFirstCaseYearID = 6;
        public const int NullSession = 7;
        public const int EmptyCircuitID = 8;
        public const int EmptySessionDate = 9;
        public const int EmptyReservationCode = 10;
        public const int NullCaseDescription = 11;
        public const int EmptyCaseDescription = 12;
        public const int EmptyLawItems = 13;
        public const int InvalidCourtID = 14;
        public const int InvalidCircuitID = 15;
        public const int EmptyCrimeID = 16;
        public const int InvalidCrimeID = 17;
        public const int EmptyProcedureTypeID = 18;
        public const int EmpyCasePartyID = 19;
        public const int EmptyCaseParties = 20;
        public const int VictimDoesntExist = 21;
        public const int DefendantDoesntExist = 22;
        public const int OnlyOneVictimDefendantExist = 23;
        public const int InvalidProcedureTypeID = 24;
        public const int InvalidFirstPoliceStationID = 25;
        public const int InvalidCaseTypeID = 26;
        public const int EmptyCaseTypeID = 27;
        public const int InvalidSessionDate = 28;
        public const int EmptyLegalPerson = 29;
        public const int EmptyOrderID = 30;
        public const int InvalidPartyType = 31;
        public const int EmptyDefendantCharges = 32;
        public const int EmptyPoliceStationStatusID = 33;
        public const int InvalidPoliceStationStatusID = 34;
        public const int EmptyCasePartyName = 35;
        public const int EmptyNationalityID = 36;
        public const int CourtIDNotRegistered = 37;
        public const int NotFoundBussinessID = 38;
    }
}
