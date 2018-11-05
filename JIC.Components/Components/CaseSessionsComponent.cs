using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class CaseSessionsComponent
    {
        private ICaseSessionsRepository CaseSessionsRepository;
        private ICircuitRollsRepository CircuitRollsRepository;
        private ICircuitsRepository CircuitsRepository;
        private IWorkDaysRepository WorkDaysRepository;

        public CaseSessionsComponent( ICaseSessionsRepository CaseSessionsRepository, ICircuitRollsRepository CircuitRollsRepository, ICircuitsRepository CircuitsRepository, IWorkDaysRepository WorkDaysRepository)
        {
            this.CaseSessionsRepository = CaseSessionsRepository;
            this.CircuitRollsRepository = CircuitRollsRepository;
            this.CircuitsRepository = CircuitsRepository;
            this.WorkDaysRepository = WorkDaysRepository;
        }

        public bool AddCaseConfiguration(vw_CaseConfiguration caseConfigurationData)
        {
            if (caseConfigurationData.SessionID == null)
            {
                long rollID;
                var result = CircuitRollsRepository.AddRoll(caseConfigurationData, out rollID);
                if (result == CircuitRollsSavestatus.Saved)
                {
                    caseConfigurationData.SessionID = rollID;
                }
            }
            return CaseSessionsRepository.AddCaseConfiguration(caseConfigurationData);
        }
        public bool EditCaseConfiguration(vw_CaseConfiguration caseConfigurationData)
        {
            if (caseConfigurationData.SessionID == null)
            {
                long rollID;
                var result = CircuitRollsRepository.AddRoll(caseConfigurationData, out rollID);
                if (result == CircuitRollsSavestatus.Saved)
                {
                    caseConfigurationData.SessionID = rollID;
                }
            }
            return CaseSessionsRepository.EditCaseConfiguration(caseConfigurationData);
        }

        public List<vw_KeyValueDate> GetCircuitRolls(int CircuitID,int? CaseType)
        {
            List<DateTime> dates = new List<DateTime>();
            List<DateTime> DatesWithoutWeekends = new List<DateTime>();
            List<vw_KeyValueDate> RemovedItems = new List<vw_KeyValueDate>();

            var rolls = CircuitRollsRepository.GetCircuitRolls(CircuitID, CaseType).ToList();
            
            foreach (var item in rolls)
            {
                dates.Add(item.Date);
            }
            DatesWithoutWeekends = RemoveWeeekends(dates);
           
            foreach (var item in rolls)
            {
                if (!DatesWithoutWeekends.Contains(item.Date))
                {
                    RemovedItems.Add(item);
                }
            }
            rolls = rolls.Except(RemovedItems).Distinct().ToList();

            return rolls;
        }
        List<DateTime> RemoveWeeekends(List<DateTime> Dates)
        {
            List<DateTime> newDates = new List<DateTime>();

            if (Dates != null)
            {
                var WorkDays = WorkDaysRepository.GetAllDays().Select(workDay => workDay.ID);
                var NonWorkDays = Enumerable.Range(0, 7).Where(nonWorkDay => !WorkDays.Contains(nonWorkDay)).Select(nonWorkDay => (DayOfWeek)nonWorkDay).ToList();

                foreach (var z in Dates)
                {
                    foreach (var k in NonWorkDays)
                    {
                        if (z.DayOfWeek != k)
                        {
                            newDates.Add(z);
                        }
                    }
                }
            }
            return newDates;
        }
        public List<vw_KeyValue> GetCircuitsByCourtID(int courtID)
        {
            return CircuitsRepository.GetCircuitsByCourtID(courtID);
        }

    }
}
