using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ICircuitRollsRepository
    {
        bool HasSession(DateTime fromDate , DateTime toDate);
        DeleteStatus DeleteCircuitRollsByCircuitID(int CircuitID);
        RollStatus OpenSessionRoll(int RollID);
        List<vw_SessionData> GetRollsOpend(int SecretaryID,int UserTypeID);
       
        List<vw_RollCases> GetSessionRollCases(int CircuitID,int SecretaryID);
        List<vw_KeyValueDate> GetCircuitRollDates(int CircuitID, int SecretaryID);
        List<vw_RollCases> GetSessionRollCasesOrder(int CircuitID, int  RollID);
        List<vw_RollCases> GetCasesINRoll(int SessionID);
        List<vw_KeyValueDate> GetCircuitRolls(int CircuitID, int? CaseTypeID = null);
        List<vw_RollCases> GetUnApprovedMovmentCases(int RollID);
        CircuitRollsSavestatus AddRoll(vw_CaseConfiguration caseConfigurationData, out long Roll_ID);
        List<vw_KeyValueDate> GetCircuitSessionDates(int CircuitID);
       bool IsRollOpened(int RollID);
       bool IsApprovedByJudge(int RollID);
       bool IsRollExist(int CycleRollID, int CircuitID, DateTime NextSessionDate);
       int GetCircuitRollID(int CycleRollID, int CircuitID, DateTime NextSessionDate);
       void AddNewCircuitRoll(int CircuitID, int CaseID, int SessionID, DateTime NextSessionDate, out long roll);
        List<vw_RollCases> GetRollSessionsForOpenedRolls(int SecretaryID);
        void UpdateRollProsecuterAndCourtHall(long rollID, int prosecuterID, int courtHall);
        DateTime GetSessionDate(int rollID);
        bool UpdateRoll(vw_CaseConfiguration vw_CaseConfiguration);
        int? GetRollID(int circuitid, DateTime SessionDate);

    }
}
