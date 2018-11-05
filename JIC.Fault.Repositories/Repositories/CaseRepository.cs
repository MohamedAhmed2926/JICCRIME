using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.Repositories
{
    public class CaseRepository : EntityRepositoryBase<Cases_Cases, vw_FaultCaseBasicData>, IFaultCaseRepository
    {

    }
}
