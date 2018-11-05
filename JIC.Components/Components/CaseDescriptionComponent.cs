using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
  public  class CaseDescriptionComponent
    {
        public ICaseDescriptionRepository DescRepository;

        public CaseDescriptionComponent(ICaseDescriptionRepository DescriptionRep)
        {
            this.DescRepository = DescriptionRep;
        }
        public vw_CaseDescription GetCaseDescriptionByCaseID(int CaseID)
        {
            return DescRepository.GetCaseDescriptionByCaseID(CaseID);
        }

        public void Add(vw_CaseDescription caseDescription)
        {
            DescRepository.Create(caseDescription);
        }
    }
}
