using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IVictimsSessionLogRepository
    {
        SaveDefectsStatus UpdateVictimsPresence(vw_CaseDefectsData Victims, int SessionID);
        bool IsPresenceSaved(int sessionID);
    }
}
