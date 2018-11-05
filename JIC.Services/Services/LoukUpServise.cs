using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;
using JIC.Services.ServicesInterfaces;

namespace JIC.Services.Services
{
    public class LookUpService : ServiceBase, ILookupService
    {

        #region Components
        public CrimTypeComponent CrimTypeComponent { get { return GetComponent<CrimTypeComponent>(); } }
        public ProsecutionsComponent ProsecutionsComponent { get { return GetComponent<ProsecutionsComponent>(); } }
        public LookupComponent lookupComponent { get { return GetComponent<LookupComponent>(); } }
        public PoliceStationComponent policeStationComponent { get { return GetComponent<PoliceStationComponent>(); } }
        public DecisionTypesComponent DecisionTypesComp { get { return GetComponent<DecisionTypesComponent>(); } }
        public TextPredictionsComponent TextPredictionsComponent { get { return GetComponent<TextPredictionsComponent>(); } }

        #endregion
        public LookUpService(CaseType caseType) : base(caseType)
        {
        }

        public List<vw_KeyValue> GetAllCitites()
        {
            return lookupComponent.GetLookup(LookupsCategories.Citites);
        }

        public List<vw_KeyValue> GetCourts()
        {
            return lookupComponent.GetCourts();
        }

        public List<vw_KeyValue> GetIntialProsecutions(int Prosecution)
        {
            return ProsecutionsComponent.GetProsecutionsFromPros(Prosecution);
        }

        public List<vw_KeyValue> GetLookupsByCategory(LookupsCategories lookupsCategory)
        {
            return lookupComponent.GetLookup(lookupsCategory);
        }

        public List<vw_KeyValue> GetPoliceStations(int ProsecutionID)
        {
            return policeStationComponent.GetPoliceStationsInPros(ProsecutionID);
        }
        public List<vw_KeyValue> GetAllPoliceStations()
        {
            return policeStationComponent.GetAllPoliceStations();
        }
        
        public List<vw_KeyValue> GetProsecutions(int? CourtID)
        {
            return ProsecutionsComponent.GetProsecutions(CourtID).ToList();
        }

        public List<vw_KeyValue> GetUserTypes()
        {
            return lookupComponent.GetUserTypes();
        }

        public List<vw_KeyValue> GetCrimeTypes(int? CourtID = null)
        {
            return CrimTypeComponent.GetCrimeType(CourtID).ToList();
        }


        public List<vw_KeyValue> GetAllCycles()
        {
            return new List<vw_KeyValue>
            {
                 new vw_KeyValue((int)Cycle.FirstCycle ,JIC.Base.Resources.Resources.FirstCycle),
                 new vw_KeyValue((int) Cycle.SecondCycle ,JIC.Base.Resources.Resources.SecondCycle),
                new vw_KeyValue((int) Cycle.ThridCycle ,JIC.Base.Resources.Resources.ThirdCycle),
                 new vw_KeyValue((int) Cycle.FourthCycle ,JIC.Base.Resources.Resources.FourthCycle)
            };
        }

        public List<vw_KeyValue> GetPartyTypes()
        {
            //متهم - مجنى عليه - متهم ومجنى عليه
            return new List<vw_KeyValue>
            {
                 //new vw_KeyValue((int)PartyTypes.All,"----اختار"),
                 new vw_KeyValue((int) PartyTypes.Victim,JIC.Base.Resources.Resources.Victim),
                new vw_KeyValue((int) PartyTypes.Defendant,JIC.Base.Resources.Resources.Defendant),
                 new vw_KeyValue((int) PartyTypes.VictimAndDefendant ,JIC.Base.Resources.Resources.VictimAndDefendant)
            };
        }

        public List<vw_KeyValue> GetJudgeTypes()
        {
            // - حكم تمهيدى - حكم قطعى
            return lookupComponent.GetLookup(LookupsCategories.CaseStatuses);
        }

        public List<vw_KeyValue> GetSessionsDateTypes()
        {

            //اخر جلسه - جلسه قادمه - لم يتم تحديد اي جلسات لها
            return new List<vw_KeyValue>
            {
               // new vw_KeyValue((int)SessionSearchMode.All,"----اختار"),
                new vw_KeyValue((int)SessionSearchMode.NotReservedSession,JIC.Base.Resources.Resources.NotReservedSession),
                new vw_KeyValue((int)SessionSearchMode.LastSessionDate,JIC.Base.Resources.Resources.LastSessionDate),
                new vw_KeyValue((int)SessionSearchMode.NextSessionDate,JIC.Base.Resources.Resources.NextSessionDateSearch)
            };
        }



        public List<vw_KeyValue> GetIntialProsecutionsByCourtID(int CourtID)
        {
            return ProsecutionsComponent.GetInitialProsInCourt(CourtID);
        }

        public List<vw_KeyValue> GetPoliceStationsByCourtID(int CourtID)
        {
            return policeStationComponent.GetPoliceStationsInCourt(CourtID);
        }

        public List<vw_KeyValue> GetCourtHalls(int? CourtID)
        {
            return lookupComponent.GetCourtHalls(CourtID);
        }

        public List<vw_KeyValue> GetDecisionTypes(CaseStatuses CaseStatuses)
        {
            return DecisionTypesComp.GetDecisionTypes(CaseStatuses);
        }

        public List<vw_KeyValue> GetJudjementTypes()
        {
            return new List<vw_KeyValue>
            {
                 new vw_KeyValue((int)CaseLevels.Initial ,JIC.Base.Resources.Resources.DecionPost ),
                 new vw_KeyValue((int) CaseLevels.Elementary ,JIC.Base.Resources.Resources.DecionFinal ),
            };
        }

        public List<vw_KeyValue> GetElementaryProsecutions(int CourtID)
        {
            return ProsecutionsComponent.GetElementaryProsecutions(CourtID);
        }

        public List<vw_KeyValue> GetCrimeTypes(int UserId)
        {
            return TextPredictionsComponent.GetCrimeTypes(UserId);
        }

       


        public List<vw_KeyValue> GetObtainmentStatuses()
        {
            return new List<vw_KeyValue>
            {
                // new vw_KeyValue((int)ObtainmentStatus.All,"----اختار"),
                 new vw_KeyValue((int) ObtainmentStatus.HasObtainment,JIC.Base.Resources.Resources.HasObtainment),
                new vw_KeyValue((int) ObtainmentStatus.NotHasObtainment,JIC.Base.Resources.Resources.NotHasObtainment),
                 
            };
        }

        public List<vw_KeyValue> GetCaseTypes()
        {
            return lookupComponent.GetCaseTypes();
        }

        public int GetNationalityIDOrCreate(string nationality)
        {
            return lookupComponent.GetLookupIDOrCreate(JIC.Base.LookupsCategories.Nationalities, nationality);
        }
    }
}
