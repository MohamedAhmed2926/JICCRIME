using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ICaseDescriptionRepository : IRepositoryBase<vw_CaseDescription,long>
    {
         vw_CaseDescription GetCaseDescriptionByCaseID(int CaseID);
    }
}
