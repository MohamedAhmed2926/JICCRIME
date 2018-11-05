using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Repositories.DBInteractions;
using JIC.Crime.Entities.Models;

namespace JIC.Crime.Repositories.Repositories
{
   public class VictimsSessionLogRepository : EntityRepositoryBase<Cases_VictimsSessionsLog>, IVictimsSessionLogRepository
    {
        public bool IsPresenceSaved(int sessionID)
        {
            return DataContext.Cases_VictimsSessionsLog.Where(vic => vic.SessionID == sessionID).Count() > 0;
        }

        public SaveDefectsStatus UpdateVictimsPresence(vw_CaseDefectsData Victims, int SessionID)
        {


            //  Cases_VictimsSessionsLog VicObj = new Cases_VictimsSessionsLog { SessionID = SessionID, VictimID = Victims.PersonID, PresenceStatusID = (int)Victims .Presence };

            var VicObj = GetAll().Where(def => def.VictimID == Victims.ID).FirstOrDefault();
            if (VicObj != null)
            {
                VicObj.SessionID = SessionID;
                VicObj.VictimID = Victims.ID;
                VicObj.PresenceStatusID = (int)Victims.Presence;
                Update(VicObj);
                Save();
                return SaveDefectsStatus.Saved_Before;
            }
            else
            {

                Cases_VictimsSessionsLog VicObject = new Cases_VictimsSessionsLog
                { SessionID = SessionID
                , VictimID = Victims.ID,
                    PresenceStatusID = (int)Victims .Presence
                };

                Add(VicObject);
                Save();
                return SaveDefectsStatus.Saved;
            }
            
        }
    }
    
}
