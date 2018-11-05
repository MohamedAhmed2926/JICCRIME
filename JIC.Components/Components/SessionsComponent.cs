using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
   public class SessionsComponent
    {

        private ISessionRepository sessionsRepository;
       // private ICaseSessionsRepository casesessionsRep;
        public SessionsComponent( ISessionRepository sessionsRepository)
        {
            this.sessionsRepository = sessionsRepository;
        }

        //public void UpdateSessionProsecuterAndCourtHall(long RollID, int ProsecuterID,int CourtHall)
        //{
        //    sessionsRepository.UpdateSessionProsecuterAndCourtHall(RollID, ProsecuterID,CourtHall);
        //}
        public SaveRollOrderStatus  SaveRollOrder(int RollID, List <vw_CaseOrder> CaseOrders)
        {
            return sessionsRepository.SaveRollOrder( RollID, CaseOrders);
        }
        public int GetRollID(int SessionID)
        {
            return sessionsRepository.GetRollID(SessionID );
        }
        public int GetCircuitID(int SessionID)
        {
            return sessionsRepository.GetCircuitID(SessionID);
        }
        public void AddSession(vw_SessionData SessionData)
        {
             sessionsRepository.AddSession(SessionData);
        }

        public bool IsAppprovedByJudge(int SessionID)
        {
           return sessionsRepository.IsApprovedByJudge( SessionID);
        }
        public vw_SessionData GetSessionData(int SessionID)
        {
            return sessionsRepository.GetSessionData(SessionID);
        }
        public void SaveMinutesOfSession(vw_MinutesOfSession MinutesOfSession)
        {
            sessionsRepository.SaveMinutesOfSession(MinutesOfSession);
        }
        public bool IsSessionMinutesSaved(int SessionID)
        {
            return sessionsRepository.IsSessionMinutesSaved(SessionID);
        }
        public void ApproveSessionByJudge(int SessionID)
        {
            sessionsRepository.ApproveSessionByJudge(SessionID);
        }
        public List<vw_SessionData> GetSessionDataByCaseID(int CaseID)
        {
            return sessionsRepository.GetSessionDataByCaseID(CaseID);
        }
        public string GetMinutesOfSession(int SessionID)
        {
            return sessionsRepository.GetMinutesOfSession(SessionID);
        }

        public void SendSessionToJudge(int SessionID)
        {
            sessionsRepository.SendSessionToJudge(SessionID);
        }
        public bool IsSentToJudge(int SessionID)
        {
           return sessionsRepository.IsSentToJudge(SessionID);
        }
        public List<int> GetRollIDs(List<int> CaseIDs)
        {
            return sessionsRepository.GetRollIDs(CaseIDs);
        }
        public IQueryable<vw_KeyValueDate> GetSessionByCircuit(int CircuitId)
        {
            return sessionsRepository.SessionByCircut(CircuitId);
        }
       public List<vw_RollCases> GetSessionsToApprove(int JudgeID, int RollID)
        {
            return sessionsRepository.GetSessionsToApprove( JudgeID, RollID);
        }

        public List<vw_KeyValueDate> GetSentToJudgeSessionDates(int JudgeID)
        {
            return sessionsRepository.GetSentToJudgeSessionDates(JudgeID);
        }

        public void ReturnSessionToSecretary(int SessionID)
        {
             sessionsRepository.ReturnSessionToSecretary(SessionID);
        }

        
    }
}
