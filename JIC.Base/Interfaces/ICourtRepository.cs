using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Base.Interfaces
{
    public interface ICourtRepository
    {
        List<vw_KeyValue> GetCourts();
        List<vw_KeyValue> GetCourtHalls(int? CourtID);
    }
}
