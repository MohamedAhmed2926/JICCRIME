using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ILookupRepository
    {
        List<vw_KeyValue> GetLookup(LookupsCategories lookup);
        List<vw_KeyValue> GetUserTypes();
        List<DecisionTypes> GetJudgeOrders_Initial(); //الحكم (امر إحاله - تحقيق...(
        List<DecisionTypes> GetJudgeOrders_Elementary(); //الحكم (براءه-ادانه...(
        List<DecisionTypes> GetDecisionTypes(); // انواع القرارات
        
        string GetNationalID(vw_CaseBasicData caseBasicData);
        List<vw_KeyValue> GetCaseTypes();
        vw_KeyValue Create(LookupsCategories lookupsCategory, string nationality);
    }
}
