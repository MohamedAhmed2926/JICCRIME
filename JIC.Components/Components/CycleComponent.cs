using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;

namespace JIC.Components.Components
{
    public class CycleComponent
    {
        public ICycleRepository CycleReposotry;


        public CycleComponent( ICycleRepository CycleReposotry)
        {
            this.CycleReposotry = CycleReposotry;
        }
        public void AddCycle(List<DateTime> list,Cycle caseType,int CourtId)
        {
            CycleReposotry.AddCycle(list, caseType, CourtId);
        }
        public List<DateTime> GetCycleDates(int courtId,Cycle cycleId)
        {
            return CycleReposotry.GetCycleDates(courtId, cycleId).ToList();
        }

        public IQueryable<DateTime> GetCycleDatesQuery(int courtID, Cycle cycle)
        {
            return CycleReposotry.GetCycleDates(courtID, cycle);
        }

        public void DeletCycle(int Year,int CourtId,Cycle cycleId)
        {
             CycleReposotry.DeleteCycle(Year, CourtId, cycleId);
        }
        public void DeletCycleByMonth(int Year, int Month, int CourtId, Cycle cycleId)
        {
            CycleReposotry.DeleteCycleByMonth(Year, Month, CourtId, cycleId);
        }

        public bool IsPreviousMonthSaved(int month, int year)
        {
            return CycleReposotry.IsPreviousMonthSaved(month, year);
        }
    }
}
