using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class DefendantCaseLogRepository : EntityRepositoryBase<Cases_DefendatnsCaseLog> , IDefendantCaseLogRepository
    {
        public void AddDefendantCaseLog(long defendantID, int defendantStatus, DateTime FromDate)
        {
            Add(new Cases_DefendatnsCaseLog
            {
                DefendantID = defendantID,
                FromDate = FromDate,
                PoliceStationStatusID = defendantStatus
            });
            Save();
        }

        public void 
            DeleteDefendantCaseLog(long DefendantID)
        {
            var DefLogs = GetAll().Where(x => x.DefendantID == DefendantID).ToList(); //GetByID(VictimID );
            foreach (var DefLog in DefLogs)
            {
                Delete(DefLog);
                Save();
            }
        }

        public void EditDefendantCaseLog(long defendantID, int defendantStatus, DateTime FromDate)
        {
            var DefendantCaseLogs = GetAllQuery().Where(defendantCaseLog => defendantCaseLog.DefendantID == defendantID && defendantCaseLog.ToDate == null).ToList();
            foreach (var caseLog in DefendantCaseLogs)
            {
                caseLog.ToDate = DateTime.Now;
                Update(caseLog);
                Save();
            }
            Add(new Cases_DefendatnsCaseLog
            {
                DefendantID = defendantID,
                FromDate = FromDate,
                PoliceStationStatusID = defendantStatus
            });
            Save();
        }
    }
}
