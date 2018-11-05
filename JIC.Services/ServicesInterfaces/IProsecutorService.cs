using JIC.Base;
using JIC.Base.views;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface IProsecutorService
    {
        ProsecutorStatus AddProsecutor(vw_ProcecuterData ProsecuterData, out int ProsecuterID);
        List<vw_ProcecuterData> GetProsecutor(int? CourtID);
        vw_ProcecuterData GetProsecutorByID(int? ProsecuterID);
        ProsecutorStatus EditProsecutor(vw_ProcecuterData Prosecuter);
        ProsecutorStatus DeleteProsecutor(int ProsecuterID);
    }
}
