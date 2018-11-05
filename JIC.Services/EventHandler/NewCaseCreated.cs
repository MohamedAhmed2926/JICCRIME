using JIC.Base.Events;
using JIC.Components.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.EventHandler
{
    public class NewCaseCreated : IEventBus<NewCase>
    {
        private CrimeCaseComponent caseComponent;
        public NewCaseCreated(CrimeCaseComponent caseComponent)
        {
            this.caseComponent = caseComponent;
        }
        public void Handle(NewCase handle)
        {
            throw new NotImplementedException();
        }
    }
}
