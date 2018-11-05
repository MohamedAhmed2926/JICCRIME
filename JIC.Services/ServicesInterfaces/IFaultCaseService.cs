using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface IFaultCaseService
    {
        CaseSaveStatus AddBasicData(vw_FaultCaseBasicData caseBasicData);
        bool AddCaseDescription(vw_CaseDescription CaseDescription);
    }
}
