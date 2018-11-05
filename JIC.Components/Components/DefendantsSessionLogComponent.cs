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
    public class DefendantsSessionLogComponent
    {
        public DefendantsSessionLogComponent( IDefendantsSessionLogRepository DefRepository)
        {
            this.DefRepository = DefRepository;
        }
        private IDefendantsSessionLogRepository DefRepository;
        public SaveDefectsStatus UpdatePresence(vw_CaseDefectsData DefectData, int SessionID)
        {
           return  DefRepository.UpdateDefentantsPresence(DefectData, SessionID);
        }
        public bool ISPresentedBefore(long DefendantID, int SessionID)
        {
            return DefRepository.ISPresentedBefore(DefendantID,SessionID);
        }

        public bool IsPresenceSaved(int SessionID)
        {
            return DefRepository.IsPresenceSaved(SessionID);

        }
    }
}
