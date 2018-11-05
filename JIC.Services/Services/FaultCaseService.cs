using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;
using JIC.Services.ServicesInterfaces;

namespace JIC.Services.Services
{
    class FaultCaseService : ServiceBase,IFaultCaseService
    {
        private readonly FaultCaseComponent CaseComponent;
        private readonly MasterCaseComponent MasterCaseComponent;
        private readonly CaseDescriptionComponent CaseDescriptionComponent;
        public FaultCaseService(FaultCaseComponent caseComponent,MasterCaseComponent MasterCaseComponent,CaseDescriptionComponent CaseDescriptionComponent) : base(CaseType.Fault)
        {
            this.CaseComponent = caseComponent;
            this.MasterCaseComponent = MasterCaseComponent;
            this.CaseDescriptionComponent = CaseDescriptionComponent;
        }

        public CaseSaveStatus AddBasicData(vw_FaultCaseBasicData caseBasicData)
        {
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {
                    caseBasicData.NationalID = MasterCaseComponent.CreateNationalID(caseBasicData);
                    int MasterCaseID;
                    CaseSaveStatus CaseResult = MasterCaseComponent.AddCaseBasicData(caseBasicData, out MasterCaseID);
                    if (CaseResult != CaseSaveStatus.Saved)
                        return CaseResult;
                    caseBasicData.MasterCaseID = MasterCaseID;
                    CaseComponent.Add(caseBasicData);
                    if (Transaction != null)
                        Transaction.Commit();
                    return CaseSaveStatus.Saved;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return CaseSaveStatus.Failed;
            }
        }

        public bool AddCaseDescription(vw_CaseDescription CaseDescription)
        {
            try
            {
                CaseDescriptionComponent.Add(CaseDescription);
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
            
        }
    }
}
