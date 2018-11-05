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
  public  class DefendantsDecisionRepository : EntityRepositoryBase<Cases_DefendantsDecision>, IDefendantsDecisionRepository
    {
        public void AddDefendantDecision(vw_DefendantsDecisionData Defendants)
        {
            Cases_DefendantsDecision DefObj= new Cases_DefendantsDecision ();
            DefObj.CaseDefendantId  = Defendants.CaseDefendantId ;
            DefObj.IsGuilty  = Defendants.IsGuilty ;
            DefObj.SessionDessionId  = Defendants.SessionDessionId ;
            DefObj.PunishmentDetails = Defendants.PunishmentDetails ;
            DefObj.PunishmentType  = Defendants.PunishmentType ;
            DefObj.RestrictionNo = Defendants.RestrictionNo ;
            DefObj.RestrictionYear  = Defendants.RestrictionYear ;
            DefObj.CreatedAt = DateTime.Now;
            DefObj.CreatedBy = "xx";
            this.Add(DefObj);
            this.Save();
           
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
