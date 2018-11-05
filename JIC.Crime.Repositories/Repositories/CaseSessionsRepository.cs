using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Crime.Repositories.Repositories
{
    public class CaseSessionsRepository : EntityRepositoryBase<Cases_CaseSessions>, ICaseSessionsRepository
    {
        public bool AddCaseConfiguration(vw_CaseConfiguration caseConfigurationData) 
        {
            foreach (var item in caseConfigurationData.CasesIDs)
            {
                Cases_CaseSessions CaseSession = new Cases_CaseSessions();
                CaseSession.CaseID = item;
                CaseSession.RollID = caseConfigurationData.SessionID.Value;
                CaseSession.DoneByDefaultCircuit = true;
                CaseSession.RollIndex = 0;
                this.Add(CaseSession);
                this.Save();
            }
            return true;
        }

        public bool EditCaseConfiguration(vw_CaseConfiguration caseConfigurationData)
        {
            foreach (var item in caseConfigurationData.CasesIDs)
            {
                //var CaseSession = this.GetByID(item);
                Cases_CaseSessions CaseSession = DataContext.Cases_CaseSessions.Where(caseid => caseid.CaseID == item).Single();
                CaseSession.RollID = caseConfigurationData.SessionID.Value;
                CaseSession.DoneByDefaultCircuit = true;
                CaseSession.RollIndex = 0;
                this.Update(CaseSession);
                this.Save();
            }
            return true;
        }
    }
}
