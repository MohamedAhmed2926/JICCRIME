using JIC.Base;
using JIC.Components.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unity;
using JIC.Repositories;
using Unity.Injection;

namespace JIC.Components.Components
{
    public class ComponentFactory
    {
        public static void InitializeContainer(CaseType caseType, IUnityContainer unityContainer)
        {
            RepositoryFactory.InitializeContainer(unityContainer, caseType);
            unityContainer.RegisterType<OverallNumberComponent>();
            unityContainer.RegisterType<DatabaseComponent>();
            unityContainer.RegisterType<AttachmentsComponent>();
            unityContainer.RegisterType<WorkDaysComponent>();
            unityContainer.RegisterType<SearchCaseComponent>();
            unityContainer.RegisterType<TextPredictionsComponent>();
            unityContainer.RegisterType<PoliceStationComponent>();
            unityContainer.RegisterType<LookupComponent>();
            unityContainer.RegisterType<VacationsComponent>();
            unityContainer.RegisterType<CircuitMembersComponent>();
            unityContainer.RegisterType<ProsecuterComponent>();
            unityContainer.RegisterType<RollsComponent>();
            unityContainer.RegisterType<OrderOfAssignmentComponent>();
            unityContainer.RegisterType<SessionsComponent>();
            unityContainer.RegisterType<CircuitsComponent>();
            unityContainer.RegisterType<LoginComponent>();
            unityContainer.RegisterType<PersonComponent>();
            unityContainer.RegisterType<CrimeCaseComponent>();
            unityContainer.RegisterType<MasterCaseComponent>();
            unityContainer.RegisterType<UserComponent>();
            unityContainer.RegisterType<CycleComponent>();
            unityContainer.RegisterType<ProsecutionsComponent>();
            unityContainer.RegisterType<DefendantsComponent>();
            unityContainer.RegisterType<VictimsComponent>();
            unityContainer.RegisterType<VictimsSessionLogComponent>();
            unityContainer.RegisterType<DefendantsSessionLogComponent>();
            unityContainer.RegisterType<PoliceStationCircuitsComponent >();
            unityContainer.RegisterType<WitnessesComponent>();
        }
    }
}
