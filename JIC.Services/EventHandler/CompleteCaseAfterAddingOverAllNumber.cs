using JIC.Components.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.EventHandler
{
    public class CompleteCaseAfterAddingOverAllNumber : IEventBus<OverAllNumberAdded>
    {
        private MasterCaseComponent CaseComponent;
        public CompleteCaseAfterAddingOverAllNumber(MasterCaseComponent caseComponent)
        {
            this.CaseComponent = caseComponent;
        }
        public void Handle(OverAllNumberAdded handle)
        {
            CaseComponent.SetCaseComplete(handle.CaseID);
        }
    }
}
