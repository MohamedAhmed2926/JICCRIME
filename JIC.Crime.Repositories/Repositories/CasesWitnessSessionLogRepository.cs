using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class CasesWitnessSessionLogRepository : EntityRepositoryBase<JIC.Crime.Entities.Models.Cases_WitnessSessionLog>, ICasesWitnessSessionLogRepository
    {
        public AddTestimonyStatus AddTestimonyToWitness(int CaseID, int SessionID, string TestimonyText, int WitnessID, byte[] FileData)
        {
            var witnessSessionLog = GetByID(WitnessID);
            try
            {
                witnessSessionLog.WitnessID = WitnessID;

                witnessSessionLog.WitnessTestimony = TestimonyText;
                witnessSessionLog.TestimonyFileData = FileData;
                this.Update(witnessSessionLog);
                this.Save();
                return AddTestimonyStatus.AddedSuccessfully;
            }
            catch(Exception ex)
            {
                return AddTestimonyStatus.FailedToAdd;
            }
        }

        public List<vw_WitnessAttendance> GetWitnessesByCaseID(int CaseID)
        {
            return (from WitnessSessionLog in DataContext.Cases_WitnessSessionLog
                    where WitnessSessionLog.CaseID == CaseID
                    select new vw_WitnessAttendance
                    {
                        AttendanceID = WitnessSessionLog.PresenceStatus,
                        WitnessID=WitnessSessionLog.WitnessID,

                    }).ToList();
        }

        public UpdatePresenceStatus UpdateWitnessesPresence(List<vw_WitnessAttendance> WitnessesAttendanceList)
        {
            try
            {
                foreach (var witness in WitnessesAttendanceList)
            {
                    Cases_WitnessSessionLog witnessSessionLog = new Cases_WitnessSessionLog();

                    witnessSessionLog.WitnessID = witness.WitnessID;
                    witnessSessionLog.CaseID = witness.CaseID;
                      witnessSessionLog.SessionID =witness.SessionID;
                    witnessSessionLog.PresenceStatus = witness.AttendanceID;
                    witnessSessionLog.WitnessTestimony = witness.WitnessTestimony;
                    witnessSessionLog.TestimonyFileData = witness.TestimonyFileData;

                  //  CaseWitness.PresenceStatus = witness.AttendanceID;
                   this.Add(witnessSessionLog);
                    this.Save();
                   
            }
                return UpdatePresenceStatus.Updated_Successfully;
            }
            catch (Exception ex)
            {
                return UpdatePresenceStatus.Failed_To_Update;
            }
        }
    }
}
