using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Components.Components;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;

namespace JIC.Services.Services
{
    public class CaseSessionsService : ServiceBase, ICaseConfiguration
    {
        private CaseSessionsComponent CaseSessionsComponent;
        public CaseSessionsService(CaseType caseType) : base(caseType)
        {
            this.CaseSessionsComponent = GetComponent<CaseSessionsComponent>();
        }

        public bool AddCaseConfiguration(vw_CaseConfiguration caseConfigurationData)
        {
            try
            {
                return CaseSessionsComponent.AddCaseConfiguration(caseConfigurationData);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
        }

        public List<vw_KeyValue> Circuits(int CourtID)
        {
            return CaseSessionsComponent.GetCircuitsByCourtID(CourtID);
        }

        public bool EditCaseConfiguration(vw_CaseConfiguration caseConfigurationData)
        {
            try
            {
                return CaseSessionsComponent.EditCaseConfiguration(caseConfigurationData);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
            throw new NotImplementedException();
        }

        public List<vw_KeyValueDate> GetCircuitRolls(int CircuitID, int? CaseType = null)
        {
            return CaseSessionsComponent.GetCircuitRolls(CircuitID,CaseType);
        }
    }
}
