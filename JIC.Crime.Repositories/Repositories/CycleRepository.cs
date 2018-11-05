using JIC.Base.Interfaces;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;

namespace JIC.Crime.Repositories.Repositories
{
    public class CycleRepository : EntityRepositoryBase<JIC.Crime.Entities.Models.CourtConfigurations_CycleRolls>, ICycleRepository
    {
        public void AddCycle(List<DateTime> dates, Cycle CycleID, int CourtID)
        {
            foreach (var item in dates)
            {
                var cycle = new JIC.Crime.Entities.Models.CourtConfigurations_CycleRolls();
                cycle.CourtID = CourtID;
                cycle.CycleID = (int)CycleID;
                cycle.RollDate = item;


                this.Add(cycle);
                this.Save();

            }
        }

        public void DeleteCycle(int Year, int CourtID, Cycle cycle)
        {
            foreach (var cycleItem in DataContext.CourtConfigurations_CycleRolls.Where(a=>a.CourtID==CourtID&& a.CycleID==(int)cycle&& a.RollDate.Year==Year))
            {
                this.Delete(cycleItem);
            }
            DataContext.SaveChanges();
        }

        public void DeleteCycleByMonth(int Year, int Month, int CourtID, Cycle cycle)
        {
            foreach (var cycleItem in DataContext.CourtConfigurations_CycleRolls.Where(a => a.CourtID == CourtID  && a.RollDate.Month == Month && a.RollDate.Year == Year).ToList())
            {
                this.Delete(cycleItem);
                DataContext.SaveChanges();
            }
        }

        public IQueryable<DateTime> GetCycleDates(int CourtID, Cycle cycle)
        {
            return (from cycleRoll in DataContext.CourtConfigurations_CycleRolls
                                   where cycleRoll.CourtID == CourtID && cycleRoll.CycleID == (int)cycle
                                   select cycleRoll).Select(a => a.RollDate);
        }

        public bool IsPreviousMonthSaved(int month, int year)
        {
            if (GetAll().Count() > 0)
            {
                return GetAll().Where(z => z.RollDate.Month == month && z.RollDate.Year == year).Count() > 0;
            }
            else
            {

                return true;
            }
        }
    }
}
