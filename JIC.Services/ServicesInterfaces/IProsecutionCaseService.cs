using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;

namespace JIC.Services.ServicesInterfaces
{
    public interface IProsecutionCaseService
    {
        bool LinkProsCase(int ProsecutionID, int CaseID);
        bool LinkProsCaseParty(int iD1, long iD2, PartyTypes defectType);
    }
}
