using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ISessionRepository
    {
      //  void UpdateSessionProsecuterAndCourtHall(long RollID, int ProsecuterID,int CourtHall);
        SaveRollOrderStatus SaveRollOrder(int RollID, List<vw_CaseOrder> CaseOrders);
        int GetRollID(int SessionID);
        vw_SessionData GetSessionData(int SessionID);
        int GetCircuitID(int SessionID);
        void AddSession(vw_SessionData SessionData);
        void SaveMinutesOfSession(vw_MinutesOfSession MinutesOfSession);
        bool IsApprovedByJudge(int SessionID);
        bool IsSessionMinutesSaved(int SessionID);
        void ApproveSessionByJudge(int SessionID);
        List<vw_SessionData> GetSessionDataByCaseID(int CaseID);
        string GetMinutesOfSession(int SessionID);
        IQueryable<vw_KeyValueDate> SessionByCircut(int CircuitId);
        void SendSessionToJudge(int sessionID);
        bool IsSentToJudge(int SessionID);
        List<int> GetRollIDs(List<int> CaseIDs);
        List<vw_RollCases> GetSessionsToApprove(int JudgeID, int RollID);
        List<vw_KeyValueDate> GetSentToJudgeSessionDates(int judgeID);
        void ReturnSessionToSecretary(int sessionID);
    }
}
