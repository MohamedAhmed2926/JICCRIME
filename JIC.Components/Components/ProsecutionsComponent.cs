using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class ProsecutionsComponent
    {
        public ProsecutionsComponent( IProsecutionsRepository ProsecutionsRepository)
        {
            this.ProsecutionsRepository = ProsecutionsRepository;
        }

        private IProsecutionsRepository ProsecutionsRepository;
        public IQueryable<JIC.Base.Views.vw_KeyValue> GetProsecutions(int? courtId)
        {
            return ProsecutionsRepository.GetProsecution(courtId);
        }

        public List<vw_KeyValue> GetProsecutionsFromPros(int prosecution)
        {
            return ProsecutionsRepository.GetProsecutionFromPros(prosecution);
        }

        public List<vw_KeyValue> GetInitialProsInCourt(int courtID)
        {
            return ProsecutionsRepository.GetInitialProsInCourt(courtID);
        }
        public List<vw_KeyValue> GetElementaryProsecutions(int CourtID)
        {
            return ProsecutionsRepository.GetElementaryProsecutions(CourtID);

        }
    }
}
