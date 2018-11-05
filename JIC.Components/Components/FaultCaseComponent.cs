using JIC.Base.Interfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
    public class FaultCaseComponent
    {
        #region Repositories
        private readonly IFaultCaseRepository faultRepository;

        #endregion
        public FaultCaseComponent(IFaultCaseRepository faultRepository)
        {
            this.faultRepository = faultRepository;
        }

        public void Add(vw_FaultCaseBasicData caseBasicData)
        {
            faultRepository.Create(caseBasicData);
        }
    }
}
