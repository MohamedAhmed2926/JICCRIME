using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Fault.Entities.Models;
using JIC.Base.Views;
using JIC.Base.Interfaces;

namespace JIC.Fault.Repositories.Repositories
{
    public class CaseDefendantProsecutioRepository:EntityRepositoryBase<Service_CaseDefendantProsecution,vw_CasePartyProsecution,long>,ICaseDefendantProsecutioRepository
    {
        
    }
}
