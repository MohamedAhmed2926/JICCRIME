using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JIC.Components.Components
{
    public class WorkDaysComponent
    {
        private IWorkDaysRepository WorkDaysRepository;

        public WorkDaysComponent( IWorkDaysRepository WorkDaysRepository)
        {
            this.WorkDaysRepository = WorkDaysRepository;
        }

        public List<vw_KeyValue> GetAllDays()
        {
            return WorkDaysRepository.GetAllDays();
        }

        public bool SaveWorkDaysList(List<vw_WorkDays> ListOfSelectedDays)
        {
            return WorkDaysRepository.SaveWorkDaysList(ListOfSelectedDays);
        }

    }
}
