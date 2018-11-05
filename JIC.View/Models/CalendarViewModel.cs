using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace JIC.Crime.View.Models
{
    public class CalendarViewModel
    {

        public CalendarViewModel()
        {

        }
        public List<DayOfWeek> NonWorkDays { get; set; } = new List<DayOfWeek>();
        public List<VacationDay> VicationDates { get; set; } = new List<VacationDay>();
        public List<DateTime> FirstCycle { get; set; } = new List<DateTime>();
        public List<DateTime> SecondCycle { get; set; } = new List<DateTime>();
        public List<DateTime> ThirdCycle { get; set; } = new List<DateTime>();
        public List<DateTime> FourthCycle { get; set; } = new List<DateTime>();
        public DateTime DefaultViewDate { get; set; } = DateTime.Today;
        public List<DateTime> Seperators { get; set; }

        public string VicationDatesJson { get { return Json.Encode(VicationDates.Select(vacation=>new { Date = GetMilliDate(vacation.Date) , Name = vacation.Vacation})); } }
        public string FirstCycleDatesJson { get { return Json.Encode(GetMilliDates(FirstCycle)); } }
        public string SecondCycleDatesJson { get { return Json.Encode(GetMilliDates(SecondCycle)); } }
        public string ThirdCycleDatesJson { get { return Json.Encode(GetMilliDates(ThirdCycle)); } }
        public string FourthCycleDatesJson { get { return Json.Encode(GetMilliDates(FourthCycle)); } }
        public string SeperatorsJson { get { return Json.Encode(GetMilliDates(Seperators)); } }

        private IEnumerable<double> GetMilliDates(List<DateTime> dates)
        {
            return dates.Select(date => GetMilliDate(date));
        }
        public double GetMilliDate(DateTime date)
        {
            return date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }

    public class VacationDay
    {
        public DateTime Date { get; set; }
        public string Vacation { get; set; }
    }
}