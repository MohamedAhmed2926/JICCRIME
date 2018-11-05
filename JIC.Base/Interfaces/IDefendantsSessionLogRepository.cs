using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IDefendantsSessionLogRepository
    {
        SaveDefectsStatus UpdateDefentantsPresence(vw_CaseDefectsData  Defentants, int SessionID);
        bool ISPresentedBefore(long DefendantID, int SessionID);
        bool IsPresenceSaved(int sessionID);
    }
}
