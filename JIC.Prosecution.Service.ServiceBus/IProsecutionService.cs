using JIC.Base.Views.ProsecutionService;
using JIC.Base.Views.Services;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace JIC.Prosecution.Service.ServiceBus
{
    [ServiceContract]
    public interface IProsecutionService
    {
        [OperationContract]
        int updateCourtHearingDateReservation(int reservationId, DateTime newReservationDate, int newCircleId);

        [OperationContract]
        string newCircle(int circleId, int courtId, DateTime?[] circleHearingDates);

        [OperationContract]
        string updateBusinessCaseDescription(int circleId, int courtId, int businessCaseId, DateTime circleDate, string newDescription);

        [OperationContract]
        string courtJudgementCompleteResolution(string resolution, int circleId, int businesscaseId, party[] parties);
        
        [OperationContract]
        string addCourtDecision(int businessCaseId, int decisionCode, string decisionText, string decisionUrl);

        [OperationContract]
        string addPartyToBusinessCase(parParties parParties);

        [OperationContract]
        string updatePartyToBusinessCase(parParties parParties);

        [OperationContract]
        string businessCaseMerging(int originalBusinessCase, int?[] mergedBusinessCases);
    }

}
