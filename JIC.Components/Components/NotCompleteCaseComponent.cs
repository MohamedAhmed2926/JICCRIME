using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class NotCompleteCaseComponent
    {
        private ICrimeCaseRepository caseRepository;
        public NotCompleteCaseComponent(ICrimeCaseRepository caseRepository)
        {
            this.caseRepository = caseRepository;
        }
        public IQueryable<vw_unCompletCase> GetNotCompleteCase(int courtId)
        {
            return caseRepository.UnCompletCase(courtId);
        }
    }
}
