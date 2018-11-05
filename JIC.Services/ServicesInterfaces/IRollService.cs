using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;

namespace JIC.Services.ServicesInterfaces
{
    public interface IRollService
    {
     
        List<vw_RollCases> GetRollCasesForOrdering(int CircuitID, int RollID );
        List<vw_RollCases> GetRollCasesForOpening(int CircuitID, int SecretaryID);
        List<vw_RollCases> GetOpenedRolls(int SecretaryID);
        List<vw_KeyValueDate> GetCircuitRollDates(int CircuitID, int SecretaryID);
        List<vw_RollCases> GetUnApprovedMovmentCases(int RollID);
        SaveRollOrderStatus SaveRollOrder(int RollID ,List<vw_CaseOrder> CaseOrders);
        List<vw_RollCases> PrintRoll(int RollID);
        SetCasesRollStatus SetCasesRoll(List<int> CaseID, int RollID);
        RollStatus OpenSessionRoll(int ProsecuterID, int CourtHall, int RollID);
        // List<vw_SessionData> GetRollsOpend(int SecretaryID);
        void UpdateRollProsecuterAndCourtHall(long RollID, int ProsecuterID, int CourtHall);
        List<vw_SessionData> GetRollsOpend(int SecretaryID, int UserTypeID);
        List<vw_RollCases> GetCasesINRoll(int SessionID);
        bool UpdateRoll(vw_CaseConfiguration vw_CaseConfiguration);

        int? GetRollID(int circuitid,DateTime SessionDate);

    }
}
