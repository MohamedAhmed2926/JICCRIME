
using JIC.Base;
using JIC.Base.Entites;
using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unity;
using Unity.Injection;

namespace JIC.Repositories
{
    public class RepositoryFactory
    {
        private CaseType caseType;
        private IUnityContainer DIContainer;

        private RepositoryFactory(CaseType caseType)
        {
            this.caseType = caseType;
            DIContainer = new UnityContainer();
            InitializeContainer(DIContainer, caseType);
        }
       

        public static void InitializeContainer(IUnityContainer DIContainer, CaseType caseType)
        {
            switch (caseType)
            {
                case CaseType.Crime:
                    DIContainer.RegisterType<IDatabaseRepository, JIC.Crime.Repositories.Repositories.DatabaseRepository>();

                    DIContainer.RegisterType<IFoldersRepository, JIC.Crime.Repositories.Repositories.FoldersRepository>();
                    DIContainer.RegisterType<IDocumentsRepository, JIC.Crime.Repositories.Repositories.DocumentsRepository>();

                    DIContainer.RegisterType<IPoliceStationsRepository, JIC.Crime.Repositories.Repositories.PoliceStationRepository>();
                    DIContainer.RegisterType<IProsecutionsRepository, JIC.Crime.Repositories.Repositories.ProsecutionsRepository>();
                    DIContainer.RegisterType<ICourtRepository, JIC.Crime.Repositories.Repositories.CourtRepository>();

                    DIContainer.RegisterType<ICircuitsRepository, JIC.Crime.Repositories.Repositories.CircuitsRepository>();
                    DIContainer.RegisterType<ICircuitRollsRepository, JIC.Crime.Repositories.Repositories.CircuitRollsRepository>();
                    DIContainer.RegisterType<ICircuitMembersRepository, JIC.Crime.Repositories.Repositories.CircuitMembersRepository>();
                    DIContainer.RegisterType<ICycleRepository, JIC.Crime.Repositories.Repositories.CycleRepository>();

                    DIContainer.RegisterType<IPersonRepository, JIC.Crime.Repositories.Repositories.PersonRepository>();
                    DIContainer.RegisterType<IVictimsRepository, JIC.Crime.Repositories.Repositories.VictimsRepository>();
                    DIContainer.RegisterType<IVictimsSessionLogRepository, JIC.Crime.Repositories.Repositories.VictimsSessionLogRepository>();
                    DIContainer.RegisterType<IDefendantsRepository, JIC.Crime.Repositories.Repositories.DefendantsRepository>();
                    DIContainer.RegisterType<IDefendantCaseLogRepository, JIC.Crime.Repositories.Repositories.DefendantCaseLogRepository>();
                    DIContainer.RegisterType<IDefendantsSessionLogRepository, JIC.Crime.Repositories.Repositories.DefendantsSessionLogRepository>();

                    DIContainer.RegisterType<IMasterCaseRepository, JIC.Crime.Repositories.Repositories.MasterCasesRepository>();
                    DIContainer.RegisterType<ICrimeCaseRepository, JIC.Crime.Repositories.Repositories.CaseRepository>();
                    DIContainer.RegisterType<IOrderOfAssignmentRepository, JIC.Crime.Repositories.Repositories.MasterCaseRepository>();
                    DIContainer.RegisterType<ISearchCaseRepository, JIC.Crime.Repositories.Repositories.SearchCaseRepository>();

                    DIContainer.RegisterType<ITextPredictionsRepository, JIC.Crime.Repositories.Repositories.TextPredictionsRepository>();
                    DIContainer.RegisterType<ILookupRepository, JIC.Crime.Repositories.Repositories.LookupRepository>();
                    DIContainer.RegisterType<ICrimeTypeRepository, JIC.Crime.Repositories.Repositories.CrimeTypeRepository>();
                    DIContainer.RegisterType<ISecurity_Users, JIC.Crime.Repositories.Repositories.Security_UsersRepository>();
                    DIContainer.RegisterType<IUserRepostiory, JIC.Crime.Repositories.Repositories.UserRepository>();
                    DIContainer.RegisterType<IProsecuterRepository, JIC.Crime.Repositories.Repositories.ProsecuterRepository>();

                    DIContainer.RegisterType<IVacationsRepository, JIC.Crime.Repositories.Repositories.VacationsRepository>();
                    DIContainer.RegisterType<IWorkDaysRepository, JIC.Crime.Repositories.Repositories.WorkDaysRepository>();
                    DIContainer.RegisterType<IOverAllNumberRepository, JIC.Crime.Repositories.Repositories.OverallNumberRepository>();

                    DIContainer.RegisterType<ICaseSessionsRepository, JIC.Crime.Repositories.Repositories.CaseSessionsRepository>();
                    DIContainer.RegisterType<IPoliceStationsCircuitsRepository, JIC.Crime.Repositories.Repositories.PoliceStationsCircuitsRepository>();

                    DIContainer.RegisterType<IDefendantChargesRepository, JIC.Crime.Repositories.Repositories.DefendantChargesRepository>();
                    DIContainer.RegisterType<IDecisionsRepository, JIC.Crime.Repositories.Repositories.DecisionsRepository>();
                    DIContainer.RegisterType<IDefendantsDecisionRepository, JIC.Crime.Repositories.Repositories.DefendantsDecisionRepository>();
                    DIContainer.RegisterType<IDecisionTypesRepository, JIC.Crime.Repositories.Repositories.DecisionTypesRepository>();
                    DIContainer.RegisterType<ISessionRepository, JIC.Crime.Repositories.Repositories.SessionRepository>();
                    DIContainer.RegisterType<ICaseWitnessesRepository, JIC.Crime.Repositories.Repositories.CaseWitnessesRepository>();
                    DIContainer.RegisterType<ICasesWitnessSessionLogRepository, JIC.Crime.Repositories.Repositories.CasesWitnessSessionLogRepository>();

                    DIContainer.RegisterType<ICaseDescriptionRepository>(new InjectionFactory((c) => null));

                    break;
                case CaseType.Fault:
                    DIContainer.RegisterType<IMasterCaseRepository, JIC.Fault.Repositories.Repositories.MasterCasesRepository>();
                    DIContainer.RegisterType<IFaultCaseRepository, JIC.Fault.Repositories.Repositories.CaseRepository>();
                    DIContainer.RegisterType<ILookupRepository, JIC.Fault.Repositories.Repositories.LookupRepository>();
                    DIContainer.RegisterType<IDatabaseRepository, JIC.Fault.Repositories.Repositories.DatabaseRepository>();
                    DIContainer.RegisterType<ICircuitsRepository, JIC.Fault.Repositories.Repositories.CircuitsRepository>();
                    DIContainer.RegisterType<ICourtRepository, JIC.Fault.Repositories.Repositories.CourtRepository>();
                    DIContainer.RegisterType<ICaseDescriptionRepository, JIC.Fault.Repositories.Repositories.CaseDescriptionRepository>();
                    DIContainer.RegisterType<ICaseSessionsRepository, JIC.Fault.Repositories.Repositories.CaseSessionsRepository>();
                    DIContainer.RegisterType<ICircuitRollsRepository, JIC.Fault.Repositories.Repositories.CircuitRollsRepository>();
                    DIContainer.RegisterType<IWorkDaysRepository, JIC.Fault.Repositories.Repositories.WorkDaysRepository>();
                    DIContainer.RegisterType<IPoliceStationsRepository, JIC.Fault.Repositories.Repositories.PoliceStationRepository>();
                    DIContainer.RegisterType<IPersonRepository, JIC.Fault.Repositories.Repositories.PersonRepository>();
                    DIContainer.RegisterType<IVictimsRepository, JIC.Fault.Repositories.Repositories.VictimsRepository>();
                    DIContainer.RegisterType<IDefendantsRepository, JIC.Fault.Repositories.Repositories.DefendantsRepository>();
                    DIContainer.RegisterType<IDefendantCaseLogRepository, JIC.Fault.Repositories.Repositories.DefendantCaseLogRepository>();
                    DIContainer.RegisterType<IDefendantChargesRepository, JIC.Fault.Repositories.Repositories.DefendantChargesRepository>();
                    DIContainer.RegisterType<IDefendantsDecisionRepository, JIC.Fault.Repositories.Repositories.DefendantsDecisionRepository>();
                    DIContainer.RegisterType<IDefendantsSessionLogRepository, JIC.Fault.Repositories.Repositories.DefendantsSessionLogRepository>();
                    DIContainer.RegisterType<ICaseProsecutionRepository, JIC.Fault.Repositories.Repositories.CaseProsecutionRepository>();
                    DIContainer.RegisterType<ICaseDefendantProsecutioRepository, JIC.Fault.Repositories.Repositories.CaseDefendantProsecutioRepository>();
                    DIContainer.RegisterType<ICaseVictimProsecutioRepository, JIC.Fault.Repositories.Repositories.CaseVictimProsecutioRepository>();

                    DIContainer.RegisterType<IOverAllNumberRepository>(new InjectionFactory((c) => null));
                    break;
            }
        }

        public static IEnumerable<Type> GetMapperProfiles(CaseType caseType)
        {
            switch (caseType)
            {
                case CaseType.Fault:
                    return new List<Type>
                    {
                        typeof(JIC.Fault.Repositories.Mapping_Porfile.FaultMapperProfile)
                    };
            }
            return new List<Type>();
        }

        public T GetRepository<T>()
        {
            return DIContainer.Resolve<T>();
        }

        public static RepositoryFactory getInstance(CaseType caseType)
        {
            return (RepositoryFactory)(HttpContext.Current.Items["RepositoryFactory"+ caseType.ToString()] ?? (HttpContext.Current.Items["RepositoryFactory" + caseType.ToString()] = new RepositoryFactory(caseType)));
        }
    }
}
