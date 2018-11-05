using AutoMapper;
using JIC.Base.Interfaces;
using JIC.Services.Services;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JIC.Services
{
    public class ServiceUnityContainer
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterSingleton<ILogger, JIC.Services.Handler.Log>();
            container.RegisterType<IDefectsService, DefectsService>();
            container.RegisterType<IUserService, Security_UsersService>();
            container.RegisterType<ILookupService, LookUpService>();
            container.RegisterType<ICircuitService, CourtConfigurationService>();
            container.RegisterType<IProsecutorService, ProsecuterService>();
            container.RegisterType<IPersonService, PersonService>();
            container.RegisterType<IVacationService, CourtConfigurationService>();
            container.RegisterType<ISearchCasesService, SearchCasesService>();
            container.RegisterType<IWorkdayService, WorkDaysService>();
            container.RegisterType<ITextPredectionsService, TextPredictionsService>();
            container.RegisterType<IOrderOfAssignment, OrderOfAssignmentService>();
            container.RegisterType<ITextPredectionsService, TextPredictionsService>();
            container.RegisterType<ICycleService, CourtConfigurationService>();
            container.RegisterType<ICaseConfiguration, CaseSessionsService>();
            container.RegisterType<IRollService, RollsService>();
            container.RegisterType<ICircuitMembersService, CircuitMembersService>();
            container.RegisterType<IAttachmentsService, AttachmentsService>();
            container.RegisterType<INotCompleteCasesService, NotCompleteCaseService>();
            container.RegisterType<IOverAllNumberService, CrimeCaseServise>();
            container.RegisterType<ISessionService, SessionsService>();
            container.RegisterType<IDecisionService, DecisionService>();
        
            container.RegisterType<ICrimeCaseService, CrimeCaseServise>();
            container.RegisterType<IPersonInformationService, PersonInformationService>();

            //Fault Service
            container.RegisterType<IFaultCaseService, FaultCaseService>();

            
            container.RegisterInstance<IMapper>(Mapper.Instance);
            container.RegisterType<IProsecutionCaseService, ProsecutionCaseService>();
        }

        public static void InitializeMapper(IEnumerable<Type> MapperProfiles)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(MapperProfiles);
            });
        }
    }
}
