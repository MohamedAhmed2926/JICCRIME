using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface ISessionService
    {
        vw_SessionData GetSessionData(int SessionID);
       SaveMinutesOfSessionStatus SaveMinutesOfSession(vw_MinutesOfSession MinutesOfSession);
      ApproveByJudgeStatus SendSessionToJudge(int SessionID ,int CaseID);
        ApproveByJudgeStatus ApproveSessionByJudge(int SessionID, int CaseID);
       
        string GetMinutesOfSession(int SessionID);
        IQueryable<vw_KeyValueDate> SessionByCicuit(int circuit);
        bool IsSessionMinutesSaved(int SessionID);
        bool IsSessionSentToJudge(int SessionID);
        List<vw_RollCases> GetSessionsToApprove(int JudgeID, int  RollID);
        List<vw_KeyValueDate> GetSentToJudgeSessionDates(int JudgeID);
        void ReturnSessionToSecretary(int sessionID);
       List<int> GetRollIDs(List<int> CaseIDs);
    }
}
