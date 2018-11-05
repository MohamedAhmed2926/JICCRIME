using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Repositories.DBInteractions;
using JIC.Fault.Entities.Models;

namespace JIC.Fault.Repositories.Repositories
{
   public class DefendantsSessionLogRepository : EntityRepositoryBase<Cases_DefendatnsSessionsLog>, IDefendantsSessionLogRepository
    {
        public bool IsPresenceSaved(int sessionID)
        {
            return DataContext.Cases_DefendatnsSessionsLog.Where(def => def.SessionID  == sessionID).Count() > 0;

        }

        public bool ISPresentedBefore(long DefendantID , int SessionID)
        {
            var defend = GetAll().Where(d => d.DefendantID == DefendantID && d.SessionID != SessionID ).ToList();
            if (defend.Count== 0)
            {
                return false;
            }
            else
            {
                return DataContext.Cases_DefendatnsSessionsLog.Where(def => def.DefendantID == DefendantID && def.PresenceStatusID != (int)PresenceStatus.AbsenceAttendance).Count() > 0;
            }
        }

        public SaveDefectsStatus UpdateDefentantsPresence(vw_CaseDefectsData  Defentants,int SessionID)
        {
            // Cases_DefendatnsSessionsLog DefObj =new Cases_DefendatnsSessionsLog { SessionID = SessionID, DefendantID = Defentants.PersonID, PresenceStatusID = (int)Defentants.Presence, CourtStatusID = Defentants.Status };
            var defend = GetAll().Where(def => def.DefendantID == Defentants.ID && def.SessionID== SessionID).FirstOrDefault();
            if (defend != null)
            {
                defend.SessionID = SessionID;
                defend.DefendantID = Defentants.ID;
                defend.PresenceStatusID = (int)Defentants.Presence;
                defend.CourtStatusID = Defentants.Status; //???
                
                Update(defend);
                Save();
                return SaveDefectsStatus.Saved_Before;
            }
            else
            {
                Cases_DefendatnsSessionsLog sessionlog = new Cases_DefendatnsSessionsLog();
          
                sessionlog.SessionID = SessionID;
                sessionlog.DefendantID = Defentants.ID;
                sessionlog.PresenceStatusID = (int)Defentants.Presence;
                sessionlog.CourtStatusID = Defentants.Status; //???
                Add(sessionlog);
                Save();
                return SaveDefectsStatus.Saved ;
            }
          
        }
    }
}
