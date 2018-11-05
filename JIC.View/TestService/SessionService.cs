using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base.Views;
using JIC.Base;

namespace JIC.Crime.View.TestService
{
    public class SessionService : ISessionService
    {
        public void ApproveSessionByJudge(int SessionID)
        {
            throw new NotImplementedException();
        }

        public string GetMinutesOfSession(int SessionID)
        {
            throw new NotImplementedException();
        }

        public SaveMinutesOfSessionStatus SaveMinutesOfSession(vw_MinutesOfSession MinutesOfSession)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_KeyValue> SessionByCicuit(int circuit)
        {
            throw new NotImplementedException();
        }

        public ApproveByJudgeStatus SendSessionToJudge(int SessionID, int CaseID)
        {
            throw new NotImplementedException();
        }

        public void UpdateSessionProsecuterAndCourtHall(long RollID, int ProsecuterID, int CourtHall)
        {
            throw new NotImplementedException();
        }

     

        vw_SessionData ISessionService.GetSessionData(int SessionID)
        {
            return new vw_SessionData()
            {
                SessionID = 1,
                SessionDate = DateTime.Now
            };
        }

      

        IQueryable<vw_KeyValueDate> ISessionService.SessionByCicuit(int circuit)
        {
            throw new NotImplementedException();
        }

        public ApproveByJudgeStatus ApproveSessionByJudge(int SessionID, int CaseID)
        {
            throw new NotImplementedException();
        }

        public bool IsSessionMinutesSaved(int SessionID)
        {
            throw new NotImplementedException();
        }

        public bool IsSessionSentToJudge(int SessionID)
        {
            throw new NotImplementedException();
        }

        public List<vw_RollCases> GetSessionsToApprove(int JudgeID, int RollID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValueDate> GetSentToJudgeSessionDates(int JudgeID)
        {
            throw new NotImplementedException();
        }

        public void ReturnSessionToSecretary(int sessionID)
        {
            throw new NotImplementedException();
        }

        public List<int> GetRollIDs(List<int> CaseIDs)
        {
            throw new NotImplementedException();
        }
    }
}