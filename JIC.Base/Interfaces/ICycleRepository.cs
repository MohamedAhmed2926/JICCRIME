using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
  public  interface ICycleRepository
    {
        void AddCycle(List<DateTime> dates, Cycle CycleID, int CourtID);

        IQueryable<DateTime> GetCycleDates(int CourtID, Cycle cycle);

        void DeleteCycle(int Year, int CourtID, Cycle cycle);
        void DeleteCycleByMonth(int Year, int Month, int CourtID, Cycle cycle);
        bool IsPreviousMonthSaved(int month, int year);
    }
}
