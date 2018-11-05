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
   public class VictimsSessionLogComponent
    {
        public VictimsSessionLogComponent( IVictimsSessionLogRepository VicRepository)
        {
            this.VicRepository = VicRepository;
        }
        private IVictimsSessionLogRepository VicRepository;
        public SaveDefectsStatus UpdatePresence(vw_CaseDefectsData DefectData, int SessionID)
        {
          return   VicRepository.UpdateVictimsPresence(DefectData, SessionID);
        }
        public bool IsPresenceSaved(int SessionID)
        {
            return  VicRepository.IsPresenceSaved( SessionID);

        }
    }
}
