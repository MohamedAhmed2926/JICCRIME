using JIC.Base.Interfaces;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Fault.Repositories.Repositories
{
  public  class DefendantsDecisionRepository : EntityRepositoryBase<Cases_DefendantsDecision, vw_DefendantsDecisionData>, IDefendantsDecisionRepository
    {
        public void AddDefendantDecision(vw_DefendantsDecisionData Defendants)
        {
            Create(Defendants);
        }
        public void DeleteDefendantDecision(int SessionID)
        {
            foreach (var DefObj in DataContext.Cases_DefendantsDecision .Where(a => a.SessionDessionId  == SessionID ).ToList())
            {
                Delete(DefObj);
                Save();
            }
          

        }

        public List<vw_DefendantsDecisionData> GetSessionDefendantsDecision(int SessionID)
        {
            List<vw_DefendantsDecisionData> DefDec=new List<vw_DefendantsDecisionData>();
          List<Cases_DefendantsDecision> Def=  GetAll().Where(z => z.SessionDessionId == SessionID).ToList();
            foreach (var x in Def)
            {
                DefDec.Add(new vw_DefendantsDecisionData {
                    IsGuilty = x.IsGuilty , CaseDefendantId=x.CaseDefendantId 
                });
            }

            return DefDec;
        }
    }
}
