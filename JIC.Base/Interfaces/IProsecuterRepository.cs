using JIC.Base.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IProsecuterRepository
    {
        ProsecutorStatus AddProsecuter(vw_ProcecuterData ProsecuterData, out int ProsecuterID);
        ProsecutorStatus EditProsecuter(vw_ProcecuterData prosecuterData);
        ProsecutorStatus DeleteProsecuter(int ProsecuterID);
        vw_ProcecuterData GetProsecutorByID(int? ProsecutorID);
        List<vw_ProcecuterData> GetProsecutors(int? courtID);
    }
}
