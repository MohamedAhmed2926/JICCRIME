using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface ICycleService
    {
        bool AddCycle(List<DateTime> dates, Cycle CycleID, int CourtID);
        bool AddCycles(List<vw_AddCycle> Cycles, int CourtID);
        List<DateTime> GetCycleDates(int CourtID, Cycle cycle);
        IQueryable<DateTime> GetCycleDatesQuery(int CourtID, Cycle cycle);
        void DeleteCycle(int Year, int CourtID, Cycle cycle);
        void DeleteCycleByMonth(int Year, int Month, int CourtID, Cycle cycle);
        bool IsPreviousMonthSaved(int month, int year);
    }
}
