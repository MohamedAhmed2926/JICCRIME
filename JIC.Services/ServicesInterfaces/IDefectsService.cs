using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface IDefectsService
    {
        List<vw_CaseDefectsData> GetDefectsByCaseID(int CaseID, int SessionID);
        List<vw_CaseDefectsData> GetDefectsByCaseID(int CaseID);
        SavePartSOrder SaveOrder(List<vw_CaseDefectData> CasePartylist);
        SaveDefectsStatus AddCaseDefect(vw_CaseDefectData CaseDefect);
        SaveDefectsStatus EditCaseDefect(vw_CaseDefectData CaseDefect);
        DeleteDefectStatus DeleteCaseDefect(int CaseID, long DefectID , PartyTypes? DefectType);
        SaveDefectsStatus UpdatePresenceOfDefects(List<vw_CaseDefectsData> DefectsList, int SessionID, out List<string> Defect);
        vw_CaseDefectsData GetCaseDefect(int caseID, long partyID, PartyTypes? partyType);
        bool IsPersonInCase(long personID, int caseID);
        bool IsPresenceSaved(int SessionID);
        CaseStatus CaseInFlow(int CaseID);
    }
}
