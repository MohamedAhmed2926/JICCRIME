using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views.Services;
using JIC.Prosecution.Service.ServiceBus.FaultService;
using JIC.Services.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Prosecution.Service.ServiceBus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FaultCourtService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FaultCourtService.svc or FaultCourtService.svc.cs at the Solution Explorer and start debugging.
    public class FaultCourtService : JIC.Prosecution.Service.Fault.IFaultCourtService
    {
        private readonly IDictionary<int, List<string>> CourtWebServices;
        private readonly ILogger logger;
        public FaultCourtService()
        {
            CourtWebServices = GetCourtWebServiceList();
            logger = Log.GetLogger();
        }


        public Response AddNewCase(NewCase Case)
        {
            try
            {
                var faultCourtServiceClients = GetFaultCourtService(Case.Court_ID);
                if (faultCourtServiceClients != null && faultCourtServiceClients.Count > 0)
                    return faultCourtServiceClients.First().AddNewCase(Case);

                return new Response
                {
                    ErrorCodes = new List<int> { ErrorCode.CourtIDNotRegistered }
                };
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return new Response
                {
                    ErrorCodes = new List<int> { ErrorCode.Failed }
                };
            }
            
        }

        public Response AddCompleteCase(CompleteCase Case)
        {
            try
            {
                var faultCourtServiceClients = GetFaultCourtService(Case.Court_ID);
                if (faultCourtServiceClients != null && faultCourtServiceClients.Count > 0)
                    return faultCourtServiceClients.First().AddCompleteCase(Case);

                return new Response
                {
                    ErrorCodes = new List<int> { ErrorCode.CourtIDNotRegistered }
                };
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Response.Failed;
            }
        }

        public Response RequestObjection(ObjectionRequest objectionRequest)
        {
            try
            {
                var faultCourtServiceClients = GetFaultCourtService(objectionRequest.Court_ID);
                if (faultCourtServiceClients == null || faultCourtServiceClients.Count == 0)
                    return new Response
                    {
                        ErrorCodes = new List<int> { ErrorCode.CourtIDNotRegistered }
                    };
                else if (faultCourtServiceClients.Count == 1)
                    return faultCourtServiceClients.First().RequestObjection(objectionRequest);
                else
                {
                    List<Task<Response>> serviceTasks = faultCourtServiceClients.Select(service => service.RequestObjectionAsync(objectionRequest)).ToList();

                    while (serviceTasks.Count > 0)
                    {
                        Task<Response> serviceTask = GetFirstFinishedTask(serviceTasks);
                        //Remove the task from the unfinished List
                        serviceTasks.Remove(serviceTask);
                        try
                        {
                            var response = serviceTask.Result;
                            if (response.Result || !response.ErrorCodes.Contains(ErrorCode.NotFoundBussinessID))
                                return response;
                        }
                        catch (OperationCanceledException) { }
                        catch (Exception ex)
                        {
                            logger.LogException(ex);
                        }
                    }
                    //if none of the services respond correctly then this id is wrong or correct service isn't registered
                    return new Response
                    {
                        ErrorCodes = new List<int> { ErrorCode.NotFoundBussinessID }
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Response.Failed;
            }
        }

        public Response RequestResumption(ResumptionRequest resumptionRequest)
        {
            try
            {
                var FaultServices = GetFaultCourtService(resumptionRequest.Court_ID);
                if (FaultServices == null || FaultServices.Count == 0)
                    return new Response
                    {
                        ErrorCodes = new List<int> { ErrorCode.CourtIDNotRegistered }
                    };
                else if (FaultServices.Count == 1)
                    return FaultServices.First().RequestResumption(resumptionRequest);
                else
                {
                    /** why can a court has more than one service !!!?
                     * This will call all Services at same time
                     * then will check each one of them if the service response is the one i need
                     * what i need is either the result is true Or it dones't have the NotFoundBussinessID error
                     * if found i will return this response
                     * example:
                     * portsaid , suez, ismalilia 
                     * each of them has an initial court 
                     * if i request an resumption i will go to the higher court
                     * sadly they added the higer court in the seperate DB (because the higher court has a branch in the city)
                     * so i have 3 primary courts in system which one of them has the original case :(
                     * so i send to all 3 at same time (Parrell so it won't wait until the previous finishs etc...) 
                     * and search in them (i like this trick actully) 
                      **/
                    List<Task<Response>> serviceTasks = FaultServices.Select(service => service.RequestResumptionAsync(resumptionRequest)).ToList();
                    
                    while (serviceTasks.Count > 0)
                    {
                        Task<Response> serviceTask = GetFirstFinishedTask(serviceTasks);
                        //Remove the task from the unfinished List
                        serviceTasks.Remove(serviceTask);
                        try
                        {
                            var response = serviceTask.Result;
                            if (response.Result || !response.ErrorCodes.Contains(ErrorCode.NotFoundBussinessID))
                                return response;
                        }
                        catch (OperationCanceledException) { }
                        catch (Exception ex)
                        {
                            logger.LogException(ex);
                        }
                    }
                    //if none of the services respond correctly then this id is wrong or correct service isn't registered
                    return new Response
                    {
                        ErrorCodes = new List<int> { ErrorCode.NotFoundBussinessID }
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return Response.Failed;
            }
            

        }

        #region Helpers
        private Task<Response> GetFirstFinishedTask(List<Task<Response>> serviceTasks)
        {
            var firstFinishedTask = Task.WhenAny(serviceTasks);
            firstFinishedTask.Wait();
            return firstFinishedTask.Result;
        }

        #endregion

        #region CourtWebServicesList

        private List<FaultCourtServiceClient> GetFaultCourtService(int CourtID)
        {
            if (CourtWebServices.ContainsKey(CourtID))
                return CourtWebServices[CourtID].Select(serviceURL => new FaultCourtServiceClient("FaultCourtService", serviceURL)).ToList();
            
            return null;

        }
        private IDictionary<int, List<string>> GetCourtWebServiceList()
        {
            return GetCourtWebServiceFromConfig();
        }

        private static IDictionary<int, List<string>> GetCourtWebServiceFromConfig()
        {
            return SystemConfigurations.FaultCourtWebServices;
        }
        #endregion
    }



}
