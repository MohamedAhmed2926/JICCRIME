using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
    public class CaseProsecutionComponent
    {
        private readonly ICaseProsecutionRepository caseProsecutionRepository;

        public CaseProsecutionComponent(ICaseProsecutionRepository caseProsecutionRepository)
        {
            this.caseProsecutionRepository = caseProsecutionRepository;
        }

        public void LinkCasePros(int CaseID, int ProsecutionID)
        {
            caseProsecutionRepository.Create(new Base.Views.vw_CaseProsecution
            {
                CaseID = CaseID,
                ProsecutionID = ProsecutionID
            });
        }

    }
}
