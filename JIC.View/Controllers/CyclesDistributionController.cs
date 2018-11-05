using JIC.Services.ServicesInterfaces;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Utilities.MvcHelpers;
using JIC.Base.Views;

namespace JIC.Crime.View.Controllers
{
    public class CyclesDistributionController : ControllerBase
    {
        public ICycleService CycleService;
        public IVacationService VacationService;
        public IWorkdayService WorkdayService;

        public CyclesDistributionController(ICycleService CycleService, IVacationService VacationService, IWorkdayService WorkdayService)
        {
            this.CycleService = CycleService;
            this.VacationService = VacationService;
            this.WorkdayService = WorkdayService;
        }
        // GET: CyclesDistribution
        public ActionResult Index(int? month, int? year)
        {
            if (CurrentUser != null)
            {
                return View(GetCycleModel(month.HasValue ? month.Value : DateTime.Today.Month, year.HasValue ? year.Value : DateTime.Today.Year, CurrentUser.CourtID.Value));
            }

                else
                {
                    return RedirectTo(Url.Action("login", "User",new {returnUrl ="/"})).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
                }
        }

        [HttpPost]
        public ActionResult Index(CyclesModel Cycles)
        {
            if (CurrentUser != null)
            {
                int CurrentMonth = new DateTime().Month;
            // if Previous month not saved
            if (!CycleService.IsPreviousMonthSaved(Cycles.FirstFrom.Value.AddMonths(-1).Month, Cycles.FirstFrom.Value.AddMonths(-1).Year))

            {
                // ModelState.AddModelError("SecondSeperatorFrom", "يجب إدخال توزيع الأدوار لجميع الأشهر السابقه");
                return RedirectTo(Url.Action("Index", new { month = Cycles.Month.Month, year = Cycles.Month.Year })).WithErrorMessages("يجب إدخال توزيع الأدوار لجميع الأشهر السابقه");

            }


            // Check that all durations are valid
            if (Cycles.FirstFrom > Cycles.FirstTo)
                ModelState.AddModelError("FirstTo", Base.Resources.Messages.Durations);
            if (Cycles.SecondFrom > Cycles.SecondTo)
                ModelState.AddModelError("SecondTo", Base.Resources.Messages.Durations);
            if (Cycles.ThirdFrom > Cycles.ThirdTo)
                ModelState.AddModelError("ThirdTo", Base.Resources.Messages.Durations);
            if (Cycles.FourthFrom > Cycles.FourthTo)
                ModelState.AddModelError("FourthTo", Base.Resources.Messages.Durations);
            if (Cycles.FirstSeperatorFrom.HasValue || Cycles.FirstSeperatorTo.HasValue)
                if (!Cycles.FirstSeperatorFrom.HasValue || !Cycles.FirstSeperatorTo.HasValue)
                    ModelState.AddModelError("FirstSeperatorFrom", "لا بد من إدخال قيمة فاصل الإنعقاد فى حالة إضافة الأخر");

            if (Cycles.FirstSeperatorFrom.HasValue && Cycles.FirstSeperatorTo.HasValue && Cycles.FirstSeperatorFrom > Cycles.FirstSeperatorTo)
                ModelState.AddModelError("FirstSeperatorFrom", Base.Resources.Messages.Durations);

            if (Cycles.SecondSeperatorFrom.HasValue || Cycles.SecondSeperatorTo.HasValue)
                if (!Cycles.SecondSeperatorFrom.HasValue || !Cycles.SecondSeperatorTo.HasValue)
                    ModelState.AddModelError("SecondSeperatorFrom", "لا بد من إدخال قيمة فاصل الإنعقاد فى حالة إضافة الأخر");

            if (Cycles.SecondSeperatorFrom.HasValue && Cycles.SecondSeperatorTo.HasValue && Cycles.FirstSeperatorFrom > Cycles.FirstSeperatorTo)
                ModelState.AddModelError("SecondSeperatorFrom", Base.Resources.Messages.Durations);

            // check that there is no intersected days between 2 cycles
            if (Cycles.FirstTo > Cycles.SecondFrom)
                ModelState.AddModelError("SecondFrom", Base.Resources.Messages.IntersectedCycles);
            if (Cycles.SecondTo > Cycles.ThirdFrom)
                ModelState.AddModelError("ThirdFrom", Base.Resources.Messages.IntersectedCycles);
            if (Cycles.ThirdTo > Cycles.FourthFrom)
                ModelState.AddModelError("FourthFrom", Base.Resources.Messages.IntersectedCycles);

            //FirstSeperator Must be Before The First Cycle
            if (Cycles.FirstSeperatorTo.HasValue && Cycles.FirstSeperatorTo >= Cycles.FirstFrom)
                ModelState.AddModelError("FirstSeperatorTo", "لا يجب ان تتقاطع ايام الفواصل مع ايام الأدوار");
            if (Cycles.SecondSeperatorFrom.HasValue && Cycles.SecondSeperatorFrom <= Cycles.FourthTo)
                ModelState.AddModelError("SecondSeperatorFrom", "لا يجب ان تتقاطع ايام الفواصل مع ايام الأدوار");

            //  on october, first day must be saturday

            if (Cycles.FirstFrom.HasValue)
            {
                if (Cycles.FirstFrom.Value.Month == 10)
                {
                    DateTime FirstSaturdayDate = GetFirstSaturdayInOctober(Cycles.FirstFrom.Value.Year);
                    if (Cycles.FirstFrom.Value != FirstSaturdayDate)
                        ModelState.AddModelError("FirstFrom", "يجب أن تبدأ السنة القضائية بأول سبت من شهر أكتوبر");
                }
            }

            // separators must be in first and last weeks only
            if (Cycles.FirstSeperatorTo.HasValue && Cycles.FirstSeperatorFrom.HasValue)
            {
                DateTime MinDate = new DateTime(Cycles.FirstSeperatorTo.Value.Year, Cycles.FirstSeperatorTo.Value.Month, 1);
                DateTime MaxDate = new DateTime(Cycles.FirstSeperatorTo.Value.Year, Cycles.FirstSeperatorTo.Value.Month, 7);

                if (!((MinDate <= Cycles.FirstSeperatorTo && Cycles.FirstSeperatorTo <= MaxDate) && (MinDate <= Cycles.FirstSeperatorFrom) && (Cycles.FirstSeperatorFrom <= MaxDate)))
                {
                    ModelState.AddModelError("FirstSeperatorTo", "يجب ان تكون فواصل الإنعقاد فى الاسبوع الاول او الاخير فقط");
                }
                if (IsThereIsUnFilledDays(Cycles.FirstSeperatorTo.Value.Date, Cycles.FirstFrom.Value.Date))
                {
                    ModelState.AddModelError("SecondSeperatorFrom", "يوجد أيام لم يتم تعريفها، يجب تعريفها لإتمام الحفظ");

                }
            }
            if (Cycles.SecondSeperatorTo.HasValue && Cycles.SecondSeperatorFrom.HasValue)
            {
                var lastDayOfMonth = DateTime.DaysInMonth(Cycles.SecondSeperatorTo.Value.Year, Cycles.SecondSeperatorTo.Value.Month);
                DateTime MinDate2 = new DateTime(Cycles.SecondSeperatorTo.Value.Year, Cycles.SecondSeperatorTo.Value.Month, lastDayOfMonth - 7);
                DateTime MaxDate2 = new DateTime(Cycles.SecondSeperatorTo.Value.Year, Cycles.SecondSeperatorTo.Value.Month, lastDayOfMonth);

                if (!((MinDate2 <= Cycles.SecondSeperatorTo) && (Cycles.SecondSeperatorTo <= MaxDate2) && (MinDate2 <= Cycles.SecondSeperatorFrom) && (Cycles.SecondSeperatorFrom <= MaxDate2)))
                {
                    ModelState.AddModelError("SecondSeperatorTo", "يجب ان تكون فواصل الإنعقاد فى الاسبوع الاول او الاخير فقط");
                }


                if (IsThereIsUnFilledDays(Cycles.SecondSeperatorFrom.Value.Date, Cycles.FourthTo.Value.Date))
                {
                    ModelState.AddModelError("SecondSeperatorFrom", "يوجد أيام لم يتم تعريفها، يجب تعريفها لإتمام الحفظ");

                }
            }

            List<DateTime> AllMonthSelectedDates = new List<DateTime>();
            AllMonthSelectedDates.AddRange(GetDatesBetween(Cycles.FirstFrom.Value, Cycles.FirstTo.Value));
            AllMonthSelectedDates.AddRange(GetDatesBetween(Cycles.SecondFrom.Value, Cycles.SecondTo.Value));
            AllMonthSelectedDates.AddRange(GetDatesBetween(Cycles.ThirdFrom.Value, Cycles.ThirdTo.Value));
            AllMonthSelectedDates.AddRange(GetDatesBetween(Cycles.FourthFrom.Value, Cycles.FourthTo.Value));

            if (Cycles.FirstSeperatorTo.HasValue && Cycles.FirstSeperatorFrom.HasValue)
            {
                AllMonthSelectedDates.AddRange(GetDatesBetween(Cycles.FirstSeperatorFrom.Value, Cycles.FirstSeperatorTo.Value));
            }
            if (Cycles.SecondSeperatorTo.HasValue && Cycles.SecondSeperatorFrom.HasValue)
            {
                AllMonthSelectedDates.AddRange(GetDatesBetween(Cycles.SecondSeperatorFrom.Value, Cycles.SecondSeperatorTo.Value));
            }


            int NumberOfWeekdEndDaysInMonth = GetNumberOfWeekEndsInAMonth(Cycles.FirstFrom.Value.Year, Cycles.FirstFrom.Value.Month);


            int AllMonthDaysCount = DateTime.DaysInMonth(Cycles.FirstFrom.Value.Year, Cycles.FirstFrom.Value.Month) - NumberOfWeekdEndDaysInMonth; // GetMonthNumberOfDays(Cycles.FirstFrom.Value.Month);


            if (AllMonthSelectedDates.Count < AllMonthDaysCount)
            {
                //ModelState.AddModelError("", JIC.Base.Resources.Messages.UnFilledDays);
                // ShowMessage(Base.MessageTypes.Error, JIC.Base.Resources.Messages.UnFilledDays);
                return RedirectTo(Url.Action("Index", new { month = Cycles.Month.Month, year = Cycles.Month.Year })).WithErrorMessages(JIC.Base.Resources.Messages.UnFilledDays);
            }



            // if there is no errors, Save cycles for the current month
            if (ModelState.IsValid)
            {
                var CycleList = new List<Base.Views.vw_AddCycle>
                {
                    new Base.Views.vw_AddCycle{ Cycle = Base.Cycle.FirstCycle,DateFrom = Cycles.FirstFrom.Value,DateTo = Cycles.FirstTo.Value},
                    new Base.Views.vw_AddCycle{ Cycle = Base.Cycle.SecondCycle,DateFrom = Cycles.SecondFrom.Value,DateTo = Cycles.SecondTo.Value},
                    new Base.Views.vw_AddCycle{ Cycle = Base.Cycle.ThridCycle,DateFrom = Cycles.ThirdFrom.Value,DateTo = Cycles.ThirdTo.Value},
                    new Base.Views.vw_AddCycle{ Cycle = Base.Cycle.FourthCycle,DateFrom = Cycles.FourthFrom.Value,DateTo = Cycles.FourthTo.Value}
                };
                if (Cycles.FirstSeperatorFrom.HasValue)
                    CycleList.Add(new Base.Views.vw_AddCycle { Cycle = Base.Cycle.FirstSeperator, DateFrom = Cycles.FirstSeperatorFrom.Value, DateTo = Cycles.FirstSeperatorTo.Value });

                if (Cycles.SecondSeperatorFrom.HasValue)
                    CycleList.Add(new Base.Views.vw_AddCycle { Cycle = Base.Cycle.SecondSeperator, DateFrom = Cycles.SecondSeperatorFrom.Value, DateTo = Cycles.SecondSeperatorTo.Value });



                var Result = CycleService.AddCycles(CycleList, CurrentUser.CourtID.Value);

                if (Result)
                    return RedirectTo(Url.Action("Index", new { month = Cycles.Month.Month, year = Cycles.Month.Year })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                else
                    ShowMessage(Base.MessageTypes.Error, JIC.Base.Resources.Messages.FaildAdd);
            }

            return View(GetCycleModel(Cycles.Month.Month, Cycles.Month.Year, CurrentUser.CourtID.Value, Cycles));
               }
                else
                {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }

        int GetNumberOfWeekEndsInAMonth(int Year , int Month )
        {
            int Count = 0;
            DateTime Date_ = new DateTime(Year, Month, 1);

            for (int i = 1; i <= DateTime.DaysInMonth(Year, Month); i++)
            {

                if (Date_.DayOfWeek==DayOfWeek.Friday )
                { Count++; }

                Date_= Date_.AddDays(1);
            }


            return Count;
        }

        bool IsThereIsUnFilledDays(DateTime SeperatorDate , DateTime CycleDate)
        {
            bool result = false;
            List<DateTime> DatesWithoutWeekends = new List<DateTime>();

            if (CycleDate > SeperatorDate)
            {
               var  startDate = new DateTime(SeperatorDate.Year, SeperatorDate.Month, SeperatorDate.Day);
              List<DateTime>  Dates = Enumerable.Range(0, 1 + CycleDate.Subtract(SeperatorDate).Days)
                       .Select(offset => startDate.AddDays(offset))
                       .ToList();

               DatesWithoutWeekends = RemoveWeeekends(Dates);
            }
            //Cycles.SecondSeperatorFrom.Value.Date , Cycles.FourthTo.Value.Date
            else
            {

                var startDate = new DateTime(CycleDate.Year, CycleDate.Month, CycleDate.Day);
                List<DateTime> Dates = Enumerable.Range(0, 1 + SeperatorDate.Subtract(CycleDate).Days)
                         .Select(offset => startDate.AddDays(offset))
                         .ToList();

                DatesWithoutWeekends = RemoveWeeekends(Dates);

            }

            if (DatesWithoutWeekends.Count > 2)
            {
                result = true;
            }
          

            return result;
        }

        List<DateTime> RemoveWeeekends(List<DateTime> Dates)
        {
            List<DateTime> newDates = new List<DateTime>();
            if (Dates != null)
            {
             
                var WorkDays = WorkdayService.GetWorkDays().Select(workDay => workDay.ID);
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

        public ActionResult CyclesDistribution(int month, int year, int courtID)
        {
            if (CurrentUser != null)
            { ViewData["SessionEnded"] = false;
                return CPartialView(GetCycleModelForMonth(month, year, courtID));
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }

        private CyclesModel GetCycleModel(int month, int year, int courtID, CyclesModel cyclesModel = null)
        {
            return (cyclesModel != null ? cyclesModel : GetCycleModelForMonth(month, year, courtID));
        }

        private void GetCyclesDates(int courtID, out List<DateTime> FirstCycleDates, out List<DateTime> SecondCycleDates, out List<DateTime> ThirdCycleDates, out List<DateTime> FourthCycleDates, out List<DateTime> Seperators)
        {
            DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            List<DateTime> Dates = Enumerable.Range(0, 17)
                    .Select(offset => startDate.AddMonths(offset))
                    .ToList();
            FirstCycleDates = new List<DateTime>();
            SecondCycleDates = new List<DateTime>();
            ThirdCycleDates = new List<DateTime>();
            FourthCycleDates = new List<DateTime>();
            Seperators = new List<DateTime>();
            foreach (var date in Dates)
            {
                CyclesModel cyclesModel = GetCycleModelForMonth(date.Month, date.Year, courtID);
                if (cyclesModel.FirstFrom.HasValue && cyclesModel.FirstTo.HasValue)
                    FirstCycleDates.AddRange(GetDatesBetween(cyclesModel.FirstFrom.Value, cyclesModel.FirstTo.Value));
                if (cyclesModel.SecondFrom.HasValue && cyclesModel.SecondTo.HasValue)
                    SecondCycleDates.AddRange(GetDatesBetween(cyclesModel.SecondFrom.Value, cyclesModel.SecondTo.Value));
                if (cyclesModel.ThirdFrom.HasValue && cyclesModel.ThirdTo.HasValue)
                    ThirdCycleDates.AddRange(GetDatesBetween(cyclesModel.ThirdFrom.Value, cyclesModel.ThirdTo.Value));
                if (cyclesModel.FourthFrom.HasValue && cyclesModel.FourthTo.HasValue)
                    FourthCycleDates.AddRange(GetDatesBetween(cyclesModel.FourthFrom.Value, cyclesModel.FourthTo.Value));
                if (cyclesModel.FirstSeperatorFrom.HasValue && cyclesModel.FirstSeperatorTo.HasValue)
                    Seperators.AddRange(GetDatesBetween(cyclesModel.FirstSeperatorFrom.Value, cyclesModel.FirstSeperatorTo.Value));
                if (cyclesModel.SecondSeperatorFrom.HasValue && cyclesModel.SecondSeperatorTo.HasValue)
                    Seperators.AddRange(GetDatesBetween(cyclesModel.SecondSeperatorFrom.Value, cyclesModel.SecondSeperatorTo.Value));
            }
        }

        private CyclesModel GetCycleModelForMonth(int month, int year, int courtID)
        {
            var CycleModel = new CyclesModel { Month = new ShowMonthOnly(month, year) };
            var FirstCycles = CycleService.GetCycleDatesQuery(courtID, Base.Cycle.FirstCycle).Where(date => date.Month == month && date.Year == year).OrderBy(date => date).ToList();
            if (FirstCycles.Count() > 0)
            {
                CycleModel.FirstFrom = FirstCycles.First();
                CycleModel.FirstTo = FirstCycles.Last();
            }

            var SecondCycles = CycleService.GetCycleDatesQuery(courtID, Base.Cycle.SecondCycle).Where(date => date.Month == month && date.Year == year).OrderBy(date => date).ToList();
            if (SecondCycles.Count() > 0)
            {
                CycleModel.SecondFrom = SecondCycles.First();
                CycleModel.SecondTo = SecondCycles.Last();
            }

            var ThirdCycles = CycleService.GetCycleDatesQuery(courtID, Base.Cycle.ThridCycle).Where(date => date.Month == month && date.Year == year).OrderBy(date => date).ToList();
            if (ThirdCycles.Count() > 0)
            {
                CycleModel.ThirdFrom = ThirdCycles.First();
                CycleModel.ThirdTo = ThirdCycles.Last();
            }

            var FourthCycles = CycleService.GetCycleDatesQuery(courtID, Base.Cycle.FourthCycle).Where(date => date.Month == month && date.Year == year).OrderBy(date => date).ToList();
            if (FourthCycles.Count() > 0)
            {
                CycleModel.FourthFrom = FourthCycles.First();
                CycleModel.FourthTo = FourthCycles.Last();
            }

            var FirstSeperator = CycleService.GetCycleDatesQuery(courtID, Base.Cycle.FirstSeperator).Where(date => date.Month == month && date.Year == year).OrderBy(date => date).ToList();
            if (FirstSeperator.Count() > 0)
            {
                CycleModel.FirstSeperatorFrom = FirstSeperator.First();
                CycleModel.FirstSeperatorTo = FirstSeperator.Last();
            }

            var SecondSeperator = CycleService.GetCycleDatesQuery(courtID, Base.Cycle.SecondSeperator).Where(date => date.Month == month && date.Year == year).OrderBy(date => date).ToList();
            if (SecondSeperator.Count() > 0)
            {
                CycleModel.SecondSeperatorFrom = SecondSeperator.First();
                CycleModel.SecondSeperatorTo = SecondSeperator.Last();
            }
            CycleModel.CourtID = CurrentUser.CourtID.Value;
            return CycleModel;
        }
        private List<DateTime> GetDatesBetween(DateTime From, DateTime To)
        {
            DateTime startDate = new DateTime(From.Year, From.Month, From.Day);
            return Enumerable.Range(0, 1 + To.Subtract(From).Days)
                    .Select(offset => startDate.AddDays(offset))
                    .ToList();
        }


        private CalendarViewModel GetCourtCalendar(int CourtID, DateTime? DefaultDate = null)
        {
            List<DateTime> FirstCycleDates, SecondCycleDates, ThirdCycleDates, FourthCycleDates, Seperators;
            GetCyclesDates(CourtID, out FirstCycleDates, out SecondCycleDates, out ThirdCycleDates, out FourthCycleDates, out Seperators);
            List<VacationDay> Vacations = new List<VacationDay>();
            foreach (var vacation in VacationService.GetVacations())
            {
                Vacations.AddRange(GetDatesBetween(vacation.VacationFrom, vacation.VacationTo).Select(_vacation => new VacationDay { Date = _vacation, Vacation = vacation.VacationName }));
            }
            var WorkDays = WorkdayService.GetWorkDays().Select(workDay => workDay.ID);
            return new CalendarViewModel
            {
                FirstCycle = FirstCycleDates,
                SecondCycle = SecondCycleDates,
                ThirdCycle = ThirdCycleDates,
                FourthCycle = FourthCycleDates,
                Seperators = Seperators,
                DefaultViewDate = (DefaultDate.HasValue ? DefaultDate.Value : DateTime.Today),
                VicationDates = Vacations,
                NonWorkDays = Enumerable.Range(0, 7).Where(nonWorkDay => !WorkDays.Contains(nonWorkDay)).Select(nonWorkDay => (DayOfWeek)nonWorkDay).ToList()
            };
        }

        public ActionResult CourtCalendar(int? month = null, int? year = null)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                return CPartialView("CourtCalendar", GetCourtCalendar(CurrentUser.CourtID.Value, (month.HasValue && year.HasValue) ? new DateTime(year.Value, month.Value, 1) : (DateTime?)null));

            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }
        DateTime GetFirstSaturdayInOctober( int year)
        {
            // Create a start date for the 1st day of the month   
            DateTime startDate = new DateTime(year,10, 1);

            // Keep looping, adding day's to the start date until Saturday is reached
            while (startDate.DayOfWeek != DayOfWeek.Saturday)
            {
                startDate = startDate.AddDays(1);
            }

            // Then return date of first Saturday of month  
            return startDate;


    }
    }
}