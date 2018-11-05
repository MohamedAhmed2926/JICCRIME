using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface IOverAllNumberService
    {
        AddOverAllStatus AddOverAllNumber(int CaseID, out long Number, out int ProsecutionNumber, out int Year , out List<AddOverAllStatus> Messages);
        AddOverAllStatus EditOverAllNumber(int CaseID, long Number, int Year);
        AddOverAllStatus DeleteOverAllNumber(int CaseID);
    }
}
