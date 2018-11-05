using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base.Views;

namespace JIC.Crime.View.TestInterfaces
{
    public class CaseConfigurationService 
    {
        public bool AddCaseConfiguration(vw_CaseConfiguration caseConfigurationData)
        {
            return true;
        }

        public List<vw_KeyValue> Circuits(int CourtID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValueDate> GetCircuitRolls(int CircuitID)
        {
            throw new NotImplementedException();
        }
    }
}