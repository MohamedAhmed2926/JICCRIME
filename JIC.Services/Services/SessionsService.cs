using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Components.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Services.Services
{
    public class SessionsService : ServiceBase,ISessionService 
    {
        public SessionsService() : base(CaseType.Crime)
        {
        }
        public SessionsComponent SessionsComp { get { return GetComponent<SessionsComponent>(); } }
        public  DefendantsSessionLogComponent DefSessionLogComp { get { return GetComponent<DefendantsSessionLogComponent>(); } }
        public VictimsSessionLogComponent VictimsSessionLogComp { get { return GetComponent<VictimsSessionLogComponent>(); } }
        public DecisionsComponent DecisionsComp { get { return GetComponent<DecisionsComponent>(); } }
        public AttachmentsComponent  AttachementComp { get { return GetComponent<AttachmentsComponent>(); } }
        public CrimeCaseComponent CaseComp { get { return GetComponent<CrimeCaseComponent>(); } }

        public ApproveByJudgeStatus ApproveSessionByJudge(int SessionID, int CaseID )
        {
            try
           {
               
                if (!DefSessionLogComp.IsPresenceSaved(SessionID) || !VictimsSessionLogComp.IsPresenceSaved(SessionID))
                {
                    return ApproveByJudgeStatus.Failed_DefectsPresence;
                }
                if (!DecisionsComp.IsDecisionSaved(SessionID))
                {
                    return ApproveByJudgeStatus.Failed_Decision;
                }
                if (!SessionsComp.IsSessionMinutesSaved(SessionID))
                {
                    return ApproveByJudgeStatus.Failed_Decision;
                }
                if (!AttachementComp.IsAttachementsSaved(CaseID))
                {
                    return ApproveByJudgeStatus.Failed_Decision;
                }

                SessionsComp.ApproveSessionByJudge(SessionID);
                // update case status after approve in table cases
                return ApproveByJudgeStatus.SessionApprovedSuccessfully;

            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ApproveByJudgeStatus.Failed;
            }
            
        }
        public ApproveByJudgeStatus SendSessionToJudge(int SessionID, int CaseID)
        {
            try
            {
                if (!AttachementComp.IsAttachementsSaved(CaseID))
                {
                    return ApproveByJudgeStatus.Failed_Attachements;
                }
                if (!DefSessionLogComp.IsPresenceSaved(SessionID) || !VictimsSessionLogComp.IsPresenceSaved(SessionID))
                {
                    return ApproveByJudgeStatus.Failed_DefectsPresence;
                }
              
                if (!SessionsComp.IsSessionMinutesSaved(SessionID))
                {
                    return ApproveByJudgeStatus.Failed_Session ;
                }

                if (!DecisionsComp.IsDecisionSaved(SessionID))
                {
                    return ApproveByJudgeStatus.Failed_Decision;
                }
                SessionsComp.SendSessionToJudge(SessionID);
            
                return ApproveByJudgeStatus.SessionSentSuccessfully;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ApproveByJudgeStatus.Failed;
            }

        }
        public vw_SessionData GetSessionData(int SessionID)
        {
          return  SessionsComp.GetSessionData(SessionID);
        }
        public string GetMinutesOfSession(int SessionID)
        {
            return SessionsComp.GetMinutesOfSession(SessionID);
        }
        public SaveMinutesOfSessionStatus SaveMinutesOfSession(vw_MinutesOfSession MinutesOfSession)
        {
            if (SessionsComp.IsAppprovedByJudge(MinutesOfSession.SessionID))
            {
                return SaveMinutesOfSessionStatus.SessionApprovedByJudge;
            }
            if (SessionsComp.IsSentToJudge(MinutesOfSession.SessionID))
            {
                return SaveMinutesOfSessionStatus.SessionSentToJudge;
            }
            else
            {
                try
                {
                    SessionsComp.SaveMinutesOfSession(MinutesOfSession);
                    return SaveMinutesOfSessionStatus.Succeeded;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    return SaveMinutesOfSessionStatus.Failed;
                }
            }
        }

        public void UpdateSessionProsecuterAndCourtHall(long RollID, int ProsecuterID,int CourtHall)
        {
           // SessionsComp.UpdateSessionProsecuterAndCourtHall(RollID, ProsecuterID,CourtHall );
        }

        public IQueryable<vw_KeyValueDate> SessionByCicuit(int circuit)
        {
            return SessionsComp.GetSessionByCircuit(circuit);
        }

        public bool IsSessionMinutesSaved(int SessionID)
        {
            return SessionsComp.IsSessionMinutesSaved(SessionID);
          
        }
        public bool IsSessionSentToJudge(int SessionID)
        {
            return SessionsComp.IsSentToJudge(SessionID);
           
        }
        public List<int> GetRollIDs(List<int> CaseIDs)
        {
            return SessionsComp.GetRollIDs(CaseIDs);
        }
        public void ReturnSessionToSecretary(int SessionID)
        {
             SessionsComp.ReturnSessionToSecretary(SessionID);

        }
        public List<vw_RollCases> GetSessionsToApprove(int JudgeID, int RollID )
        {
            return SessionsComp.GetSessionsToApprove(JudgeID, RollID);
        }

        public List<vw_KeyValueDate> GetSentToJudgeSessionDates(int JudgeID)
        {
            return SessionsComp.GetSentToJudgeSessionDates( JudgeID);
        }
    }
}
