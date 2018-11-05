using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface ILookupService
    {
        List<vw_KeyValue> GetProsecutions(int? CourtID);

        List<vw_KeyValue> GetIntialProsecutions(int Prosecution);
        List<vw_KeyValue> GetUserTypes();
        List<vw_KeyValue> GetCourts();
        List<vw_KeyValue> GetCourtHalls(int? CourtID);
        List<vw_KeyValue> GetLookupsByCategory(LookupsCategories judgLevel);
        List<vw_KeyValue> GetAllCitites();
        List<vw_KeyValue> GetAllPoliceStations();
        List<vw_KeyValue> GetPoliceStations(int ProsecutionID);
        List<vw_KeyValue> GetCrimeTypes(int? CourtID = null);
        List<vw_KeyValue> GetCrimeTypes(int UserId);
        List<vw_KeyValue> GetPartyTypes();
        List<vw_KeyValue> GetJudgeTypes();

        List<vw_KeyValue > GetDecisionTypes(CaseStatuses caseStatuses); // انواع القرارات و الاحكام
        List<vw_KeyValue> GetJudjementTypes(); // قرار\حكم

        List<vw_KeyValue> GetSessionsDateTypes();

        List<vw_KeyValue> GetAllCycles();

        List<vw_KeyValue> GetIntialProsecutionsByCourtID(int CourtID);
        List<vw_KeyValue> GetPoliceStationsByCourtID(int CourtID);
        List<vw_KeyValue> GetElementaryProsecutions(int CourtID);
        List<vw_KeyValue> GetObtainmentStatuses();
        List<vw_KeyValue> GetCaseTypes();
        int GetNationalityIDOrCreate(string nationality);
    }
}
