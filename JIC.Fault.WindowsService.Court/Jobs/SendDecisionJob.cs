using JIC.Base.Interfaces;
using JIC.Services.ServicesInterfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.WindowsService.Court.Jobs
{
    public class SendDecisionJob : IJob
    {
        private readonly ProsecutionService.ProsecutionServiceClient prosecutionServiceClient;
        private readonly ILogger logger;
        public SendDecisionJob(ILogger logger,IFaultCaseService caseService)
        {
            prosecutionServiceClient = new ProsecutionService.ProsecutionServiceClient();
            this.logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("Executing Service");
            return Task.Run(() => SendDecision());
        }

        public void SendDecision()
        {
            try
            {
                //Todo this should get all decision that needs to be sent and send them one by one 
                //if decision postpost call postpone function
                //if decision complete call complete
                //if decision stop case
                //if decision open case
                // loop until you finish

                //example
                var response = prosecutionServiceClient.addCourtDecision(0, 0, "test", null);
                //set caseDescision has Been Set
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
        }
    }
}
