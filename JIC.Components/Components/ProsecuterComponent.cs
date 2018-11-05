using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.views;

namespace JIC.Components.Components
{
    public class ProsecuterComponent
    {
        private IProsecuterRepository ProsecuterRepository;

        public ProsecuterComponent( IProsecuterRepository ProsecuterRepository)
        {
            this.ProsecuterRepository = ProsecuterRepository;
        }

        public ProsecutorStatus AddProsecuter(vw_ProcecuterData ProsecuterData, out int ProsecuterID)
        {
            return ProsecuterRepository.AddProsecuter(ProsecuterData, out ProsecuterID);
        }
        public ProsecutorStatus EditProsecuter(vw_ProcecuterData prosecuterData)
        {
            return ProsecuterRepository.EditProsecuter(prosecuterData);
        }
        public ProsecutorStatus DeleteProsecuter(int ProsecuterID)
        {
            return ProsecuterRepository.DeleteProsecuter(ProsecuterID);
        }
        public vw_ProcecuterData GetProsecutorByID(int? ProsecuterID)
        {
            return ProsecuterRepository.GetProsecutorByID(ProsecuterID);
        }

        public List<vw_ProcecuterData> GetProsecutors(int? courtID)
        {
            return ProsecuterRepository.GetProsecutors(courtID);
        }
    }
}



