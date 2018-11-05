using AutoMapper;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views.ProsecutionService;
using JIC.Services.Handler;
using System;

namespace JIC.Prosecution.Service.ServiceBus
{
    public class ProsecutionService : IProsecutionService
    {
        private readonly PublicProsecutionService.CourtServicesClient courtServicesClient;
        private readonly ILogger logger;
        public ProsecutionService()
        {
            courtServicesClient = new PublicProsecutionService.CourtServicesClient();
            this.logger = Log.GetLogger();
        }
        #region Helpers
        public string Failed { get { return SystemConfigurations.FailedProsResponse; } }
        public int FailedInt { get { return SystemConfigurations.FailedProsResponseInt; } }
        #endregion
        #region Circles
        public string newCircle(int circleId, int courtId, DateTime?[] circleHearingDates)
        {
            try
            {
                return courtServicesClient.newCircle(circleId, courtId, circleHearingDates);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Failed;
            }
        }
        #endregion

        #region Parties
        public string addPartyToBusinessCase(parParties parParties)
        {
            try
            {
                return courtServicesClient.addPartyToBusinessCase(parParties.toPPoParParties());
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Failed;
            }
        }

        public string updatePartyToBusinessCase(parParties parParties)
        {
            try
            {
                return courtServicesClient.updatePartyToBusinessCase(parParties.toPPoParParties());
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Failed;
            }
        }
        #endregion

        #region MergeCases
        public string businessCaseMerging(int originalBusinessCase, int?[] mergedBusinessCases)
        {
            try
            {
                return courtServicesClient.businessCaseMerging(originalBusinessCase, mergedBusinessCases);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Failed;
            }
        }

        #endregion

        #region CaseDescription
        public string updateBusinessCaseDescription(int circleId, int courtId, int businessCaseId, DateTime circleDate, string newDescription)
        {
            try
            {
                return courtServicesClient.updateBusinessCaseDescription(circleId, courtId, businessCaseId, circleDate, newDescription);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Failed;
            }
        }
        #endregion

        #region Case Descision

        #region PostPone
        /// <summary>
        /// update the case with the next session
        /// </summary>
        /// <param name="reservationId"></param>
        /// <param name="newReservationDate"></param>
        /// <param name="newCircleId"></param>
        /// <returns>new reservation id for Case</returns>
        public int updateCourtHearingDateReservation(int reservationId, DateTime newReservationDate, int newCircleId)
        {
            try
            {
                return courtServicesClient.updateCourtHearingDateReservation(reservationId, newReservationDate, newCircleId);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return FailedInt;
            }
        }
        #endregion

        #region Decision
        /// <summary>
        /// send Decision Text
        /// </summary>
        /// <param name="businessCaseId">Pros Case ID</param>
        /// <param name="decisionCode"></param>
        /// <param name="decisionText">Decision Text</param>
        /// <param name="decisionUrl">Decision File</param>
        /// <returns>success | failed</returns>
        public string addCourtDecision(int businessCaseId, int decisionCode, string decisionText, string decisionUrl)
        {
            try
            {
                return courtServicesClient.addCourtDecision(businessCaseId, decisionCode, decisionText, decisionUrl);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Failed;
            }
        }

        #endregion

        #region Final Decision
        /// <summary>
        /// sending Final Decision
        /// </summary>
        /// <param name="resolution">Complete (Final) Decision Text</param>
        /// <param name="circleId">----</param>
        /// <param name="businesscaseId">Pros Case ID</param>
        /// <param name="parties">CasePartyID(Pros) - Presence</param>
        /// <returns>sucess | failed</returns>
        public string courtJudgementCompleteResolution(string resolution, int circleId, int businesscaseId, JIC.Base.Views.ProsecutionService.party[] parties)
        {
            try
            {
                return courtServicesClient.courtJudgementCompleteResolution(resolution, circleId, businesscaseId, parties.toPPOParty());
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Failed;
            }
        }

        #endregion

        #endregion
        
    }

}
