using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface ICaseConfiguration
    {
        bool AddCaseConfiguration(vw_CaseConfiguration caseConfigurationData);
        bool EditCaseConfiguration(vw_CaseConfiguration caseConfigurationData);
        List<vw_KeyValue> Circuits(int CourtID);
        List<vw_KeyValueDate> GetCircuitRolls(int CircuitID,int? CaseTypeID = null);

    }
}
