using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface IWorkdayService
    {
        bool AddWorkDays(List<vw_WorkDays> days);
        List<vw_KeyValue> GetWorkDays();
    }
}
