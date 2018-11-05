using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base.Views;

namespace JIC.Crime.View.TestServiceI
{
    public class WorkdayService : IWorkdayService
    {
        public bool AddWorkDays(List<DayOfWeek> days)
        {
            return true;
        }

        public void DeleteWorkDay(DayOfWeek day, int CourtID)
        {
            throw new NotImplementedException();
        }

        public void DeleteWorkDays(List<DayOfWeek> days, int CourtID)
        {
            throw new NotImplementedException();
        }

        public List<DayOfWeek> GetWorkDays(int CourtID)
        {
            return new List<DayOfWeek>()
            {
                 DayOfWeek.Friday,
                 DayOfWeek.Wednesday
            };
        }

        public List<DayOfWeek> GetWorkDays()
        {
            //return null;
            return new List<DayOfWeek>()
            {
                 DayOfWeek.Sunday,
                 DayOfWeek.Monday,
                 DayOfWeek.Tuesday,
                 DayOfWeek.Wednesday,
                 DayOfWeek.Thursday
            };
        }

        public bool AddWorkDays(List<vw_WorkDays> days)
        {
            throw new NotImplementedException();
        }

        List<vw_KeyValue> IWorkdayService.GetWorkDays()
        {
            throw new NotImplementedException();
        }
    }
}