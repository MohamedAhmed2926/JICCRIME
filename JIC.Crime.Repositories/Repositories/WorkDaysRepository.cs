using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class WorkDaysRepository : EntityRepositoryBase<CourtConfigurations_WorkDays>, IWorkDaysRepository
    {
        public List<vw_KeyValue> GetAllDays()
        {
            return ( from days in DataContext.CourtConfigurations_WorkDays
                     select new vw_KeyValue
                     {
                         ID = days.ID,
                         Name = days.DayOfWeek
                     }
                ).ToList();
        }

        public bool SaveWorkDaysList(List<vw_WorkDays> ListOfSelectedDays)
        {
            DataContext.CourtConfigurations_WorkDays.ToList().ForEach(a => DataContext.CourtConfigurations_WorkDays.Remove(a));
            this.Save();

            foreach (var item in ListOfSelectedDays)
            {
                CourtConfigurations_WorkDays obj = new CourtConfigurations_WorkDays
                {
                    DayOfWeek = item.WorkDay,
                    ID = item.ID
                };
                this.Add(obj);
                this.Save();
            }
            return true;
        }
    }
}
