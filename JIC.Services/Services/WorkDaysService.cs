using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Components.Components;

namespace JIC.Services.Services 
{
    public class WorkDaysService : ServiceBase,IWorkdayService

    {
        public WorkDaysComponent WorkDaysComponent;
        public WorkDaysService(CaseType caseType) : base(caseType)
        {
            WorkDaysComponent = GetComponent<WorkDaysComponent>();
        }

        public bool AddWorkDays(List<vw_WorkDays> days)
        {
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {
                    var Result = WorkDaysComponent.SaveWorkDaysList(days);
                    if (Result)
                        if (Transaction != null)
                            Transaction.Commit();
                    return Result;
                }
            }catch(Exception ex)
            {
                HandleException(ex);
                return false;
            }
        }

        public List<vw_KeyValue> GetWorkDays()
        {
            return WorkDaysComponent.GetAllDays();
        }
    }
}
