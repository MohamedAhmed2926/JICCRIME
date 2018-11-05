using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface  IDecisionsRepository
    {
        SaveDecisionStatus SaveDecision(vw_CaseDecision DecisionData);
        List<DecisionTypes> GetJudgeTypes(); //نوع الحكم(قطعى-تمهيدى)
        List<DecisionTypes> GetJudgeOrders(); //الحكم (امر إحاله - تحقيق...(
        List<DecisionTypes> GetDecisionTypes(); // انواع القرارات
    }
}
