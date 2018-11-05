using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Services.Services;
using JIC.Services.ServicesInterfaces;
using JIC.Base.views;
using JIC.Components.Components;
using JIC.Base.Views;

namespace JIC.Services.Services
{
    public class ProsecuterService : ServiceBase, IProsecutorService

    {
        public ProsecuterService(CaseType caseType) : base(caseType)
        {
        }
        public  ProsecuterComponent  ProsecuterComponent { get { return GetComponent<ProsecuterComponent>(); } }

        public ProsecutorStatus AddProsecutor(vw_ProcecuterData ProsecuterData, out int ProsecuterID)
        {
           return ProsecuterComponent.AddProsecuter(ProsecuterData , out ProsecuterID);
        }

        public ProsecutorStatus DeleteProsecutor(int ProsecuterID)
        {
            return ProsecuterComponent.DeleteProsecuter(ProsecuterID);
        }

        public ProsecutorStatus EditProsecutor(vw_ProcecuterData Prosecuter)
        {
            return ProsecuterComponent.EditProsecuter(Prosecuter);
        }
        

        public List<vw_ProcecuterData> GetProsecutor(int? CourtID)
        {
            return ProsecuterComponent.GetProsecutors(CourtID);
        }
 

        public vw_ProcecuterData GetProsecutorByID(int? ProsecuterID)
        {
            return ProsecuterComponent.GetProsecutorByID(ProsecuterID);
        }
    }
}
