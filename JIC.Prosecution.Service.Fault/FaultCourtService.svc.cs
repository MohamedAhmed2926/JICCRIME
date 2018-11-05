using JIC.Base;
using JIC.Base.Entites;
using JIC.Base.Interfaces;
using JIC.Base.Views.Services;
using JIC.Prosecution.Service.Fault.Validation;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Attributes;

namespace JIC.Prosecution.Service.Fault
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FaultCourtService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FaultCourtService.svc or FaultCourtService.svc.cs at the Solution Explorer and start debugging.
    public class FaultCourtService : IFaultCourtService
    {
        private readonly IFaultCaseService caseService;
        private readonly NewCaseValidator caseValidator;
        private readonly CompleteCaseValidator completeCaseValidator;
        private readonly IDefectsService defectsService;
        private readonly IPersonService personService;
        private readonly ICaseConfiguration CaseConfiguration;
        private readonly IDatabaseRepository databaseRepository;
        private readonly ILookupService lookupService;
        private readonly ILogger logger;
        private readonly IProsecutionCaseService prosecutionCaseService;
        public FaultCourtService(
            IFaultCaseService caseService,
            NewCaseValidator caseValidator,
            CompleteCaseValidator completeCaseValidator,
            IDefectsService defectsService,
            IPersonService personService,
            ILookupService lookupService,
            ICaseConfiguration CaseConfiguration,
            IDatabaseRepository databaseRepository,
            ILogger logger,
            IProsecutionCaseService prosecutionCaseService)
        {
            this.caseService = caseService;
            this.caseValidator = caseValidator;
            this.completeCaseValidator = completeCaseValidator;
            this.defectsService = defectsService;
            this.personService = personService;
            this.databaseRepository = databaseRepository;
            this.CaseConfiguration = CaseConfiguration;
            this.lookupService = lookupService;
            this.logger = logger;
            this.prosecutionCaseService = prosecutionCaseService;
        }

        /// <summary>
        /// Add new Case to System 
        /// and reserve a date for it
        /// </summary>
        /// <param name="Case">All Case MetaData</param>
        /// <returns>Response if Saved or Not with error codes</returns>
        public Response AddNewCase(NewCase Case)
        {

            try
            {
                //Validate Pros Data
                //This will Be our big player
                //if any thing passes this validation
                //Data could be wrong
                if (!caseValidator.CValidate(Case, out List<int> ErrorCodes))
                    return ReturnErrorResponse(ErrorCodes);
                using (var Transaction = databaseRepository.BeginTransaction())
                {
                    //Save Basic Data First
                    Base.Views.vw_FaultCaseBasicData caseBasicData = Case.MapToBasicData();
                    var caseSaveStatus = caseService.AddBasicData(caseBasicData);
                    if (caseSaveStatus != CaseSaveStatus.Saved)
                        return ReturnErrorResponse(caseSaveStatus);

                    if (!prosecutionCaseService.LinkProsCase(Case.Business_Case_Id, caseBasicData.CaseID))
                        return Response.Failed;

                    //Save Case Description
                    if (!caseService.AddCaseDescription(Case.MapCaseDescription(caseBasicData.CaseID)))
                        return Response.Failed;

                    //Save First Session Date
                    long SessionID = GetSessionID(Case.First_Session);
                    if (!CaseConfiguration.AddCaseConfiguration(Case.MapCaseSessionConfiguration(SessionID, caseBasicData.CaseID)))
                        return Response.Failed;

                    //Todo Save CaseParty Data
                    foreach (var CaseParty in Case.CaseParties)
                    {
                        //this will check if the nationalityExist or not and if not create it
                        CaseParty.NationalityID = lookupService.GetNationalityIDOrCreate(CaseParty.Nationality);
                        var PersonSaveStatus = personService.AddPerson(CaseParty.MapPersonData(), out long PersonID);
                        if (PersonSaveStatus != PersonStatus.SuccefullSave)
                            return ReturnErrorResponse(PersonSaveStatus);

                        SaveDefectsStatus caseDefectSaveStatus = this.defectsService.AddCaseDefect(CaseParty.MapCaseDefectData(caseBasicData.CaseID, PersonID));
                        if (caseDefectSaveStatus != SaveDefectsStatus.Saved)
                            return ReturnErrorResponse(caseDefectSaveStatus);

                         var Defects = defectsService.GetDefectsByCaseID(caseBasicData.CaseID)
                            .Where(caseDefect => caseDefect.PersonID == PersonID);

                        foreach (var caseDefect in Defects)
                        {
                            if (!prosecutionCaseService.LinkProsCaseParty(CaseParty.ID, caseDefect.ID, caseDefect.DefectType))
                                return Response.Failed;
                        }
                    }
                    //Todo Save Attachments
                    if(Transaction != null)
                    Transaction.Commit();
                    return Response.Success;

                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Response.Failed;
            }
        }

        /// <summary>
        /// Save a complete Case to the System
        /// </summary>
        /// <param name="Case">All Case MetaData</param>
        /// <returns>Response if Saved or Not with error codes</returns>
        public Response AddCompleteCase(CompleteCase Case)
        {

            try
            {
                using (var Transaction = databaseRepository.BeginTransaction())
                {
                    //Validate Pros Data
                    //This will Be our big player
                    //if any thing passes this validation
                    //Data could be wrong
                    if (!completeCaseValidator.CValidate(Case, out List<int> ErrorCodes))
                        return ReturnErrorResponse(ErrorCodes);

                    //Save Basic Data First
                    Base.Views.vw_FaultCaseBasicData vw_FaultCaseBasicData = Case.MapToBasicData();
                    var caseSaveStatus = caseService.AddBasicData(vw_FaultCaseBasicData);
                    if (caseSaveStatus != CaseSaveStatus.Saved)
                        return ReturnErrorResponse(caseSaveStatus);

                    //Save Case Description
                    if (!caseService.AddCaseDescription(Case.MapCaseDescription(vw_FaultCaseBasicData.CaseID)))
                        return Response.Failed;

                    //Save First Session Date
                    long SessionID = GetSessionID(Case.FinalSession);
                    if (!CaseConfiguration.AddCaseConfiguration(Case.MapCaseSessionConfiguration(SessionID, vw_FaultCaseBasicData.CaseID)))
                        return Response.Failed;

                    //Todo Save CaseParty Data
                    foreach (var CaseParty in Case.CaseParties)
                    {
                        var PersonSaveStatus = personService.AddPerson(CaseParty.MapPersonData(), out long PersonID);
                        if (PersonSaveStatus != PersonStatus.SuccefullSave)
                            return ReturnErrorResponse(PersonSaveStatus);

                        SaveDefectsStatus caseDefectSaveStatus = this.defectsService.AddCaseDefect(CaseParty.MapCaseDefectData(vw_FaultCaseBasicData.CaseID, PersonID));
                        if (caseDefectSaveStatus != SaveDefectsStatus.Saved)
                            return ReturnErrorResponse(caseDefectSaveStatus);

                        //Todo Save Decision
                    }
                    //Todo Save Attachments

                    Transaction.Commit();
                    return Response.Success;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Response.Failed;
            }
        }    


        public Response RequestObjection(ObjectionRequest objectionRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Request a Resumption for this Case
        /// </summary>
        /// <param name="resumptionRequest"></param>
        /// <returns></returns>
        public Response RequestResumption(ResumptionRequest resumptionRequest)
        {
            return ReturnErrorResponse(new List<int> { ErrorCode.NotFoundBussinessID });
        }

        #region Helpers


        private Response ReturnErrorResponse(PersonStatus personSaveStatus)
        {
            return ReturnErrorResponse(new List<int> { (int)personSaveStatus });
        }

        private long GetSessionID(CaseSession FirstSession)
        {
            return CaseConfiguration.GetCircuitRolls(FirstSession.Circuit_ID,FirstSession.CaseTypeID)
                                    .Where(circuitDate => circuitDate.Date == FirstSession.Session_Date)
                                    .Select(circuitDate => circuitDate.ID)
                                    .First();
        }

        private Response ReturnErrorResponse(SaveDefectsStatus caseDefectSaveStatus)
        {
            return ReturnErrorResponse(new List<int> { (int)caseDefectSaveStatus });
        }

        private Response ReturnErrorResponse(List<int> errorCodes)
        {
            if (errorCodes == null)
                errorCodes = new List<int> { ErrorCode.Failed };
            return new Response
            {
                ErrorCodes = errorCodes 
            };
        }

        private Response ReturnErrorResponse(CaseSaveStatus caseSaveStatus)
        {
            return ReturnErrorResponse(new List<int> { (int) caseSaveStatus });
        }
        #endregion

    }
}
