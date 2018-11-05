using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Crime.Repositories;
using JIC.Services.Services;
using System;
using System.Web;
using Unity;
using Unity.Injection;
using Microsoft.AspNet.Identity.Owin;


namespace JIC.Crime.View
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            container.RegisterInstance(CaseType.Crime);

            container.RegisterType<IDefectsService, DefectsService>();
            container.RegisterType<IUserService, Security_UsersService>();
            container.RegisterType<ILookupService, LookUpService>();
            container.RegisterType<ICircuitService, CourtConfigurationService>();
            container.RegisterType<IProsecutorService, ProsecuterService>();
            container.RegisterType<IPersonService, PersonService>();
            container.RegisterType<IVacationService, CourtConfigurationService>();
            container.RegisterType<ISearchCasesService, SearchCasesService>();
            container.RegisterType<IWorkdayService, WorkDaysService>();
            container.RegisterType<ICrimeCaseService, CrimeCaseServise>();
            container.RegisterType<ITextPredectionsService, TextPredictionsService>();
            container.RegisterType<IOrderOfAssignment, OrderOfAssignmentService>();
            container.RegisterType<ITextPredectionsService, TextPredictionsService>();
            container.RegisterType<ICycleService, CourtConfigurationService>();
            container.RegisterType<ICaseConfiguration, CaseSessionsService>();
            container.RegisterType<IRollService,RollsService>();
            container.RegisterType<ICircuitMembersService, CircuitMembersService>();
            container.RegisterType<IAttachmentsService, AttachmentsService>();
            container.RegisterType<INotCompleteCasesService, NotCompleteCaseService>();
            container.RegisterType<IOverAllNumberService, CrimeCaseServise>();
            container.RegisterType<ISessionService, SessionsService>();
            container.RegisterType<IDecisionService, DecisionService>();
            container.RegisterType<IWitnessesService , WitnessesService>();
            container.RegisterType<IWitnessSessionLogService, WitnessSessionLogService>();
            ////Test Service
            container.RegisterType<ICircuitConfigurationService, TestService.CircuitConfigurationService>();

        }

        public static T GetService<T>()
        {
            return Container.Resolve<T>();
        }
        public static object GetObject(Type type)
        {
            return Container.Resolve(type);
        }

    }
}