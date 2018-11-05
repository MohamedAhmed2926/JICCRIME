using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ICaseSessionsRepository
    {
        bool AddCaseConfiguration(vw_CaseConfiguration caseConfigurationData);
        bool EditCaseConfiguration(vw_CaseConfiguration caseConfigurationData);
       
    }
}
