using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Base;
using JIC.Crime.View.Helpers;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.ElementaryCourtAdministrator)]
    public class WorkingDaysController : ControllerBase
    {
        private IWorkdayService workdayService;

        public WorkingDaysController(IWorkdayService workdayService)
        {
            this.workdayService = workdayService;
        }
        // GET: WorkingDays
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<vw_KeyValue> WorkDays = workdayService.GetWorkDays();
            List<DayOfWeek> WorkingDays = new List<DayOfWeek>();
            foreach (vw_KeyValue WD in WorkDays)
            {
                WorkingDays.Add((DayOfWeek)WD.ID);
            }
            if (WorkingDays != null && WorkingDays.Count > 0)
            {
                WorkingDaysViewModels model = new WorkingDaysViewModels();
                foreach (var day in WorkingDays)
                {
                    if (day == DayOfWeek.Friday)
                        model.IsFridaySelected = false;
                    if (day == DayOfWeek.Saturday)
                        model.IsSaturdaySelected = false;
                    if (day == DayOfWeek.Sunday)
                        model.IsSundaySelected = false;
                    if (day == DayOfWeek.Monday)
                        model.IsMondaySelected = false;
                    if (day == DayOfWeek.Tuesday)
                        model.IsTuesdaySelected = false;
                    if (day == DayOfWeek.Wednesday)
                        model.IsWednesdaySelected = false;
                    if (day == DayOfWeek.Thursday)
                        model.IsThursdaySelected = false;
                }
                return View(model);
            }

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkingDaysViewModels model)
        {
            if (ModelState.IsValid)
            {
                //List<DayOfWeek> SelectedWorkDays = new List<DayOfWeek>();
                List<vw_WorkDays> SelectedWorkDays = new List<vw_WorkDays>();
                if (!model.IsFridaySelected)
                {
                    SelectedWorkDays.Add(new vw_WorkDays() { ID = 5, WorkDay = "الجمعة" });
                }
                if (!model.IsSaturdaySelected)
                {
                    SelectedWorkDays.Add(new vw_WorkDays() { ID = 6, WorkDay = "السبت" });
                }
                if (!model.IsSundaySelected)
                {
                    SelectedWorkDays.Add(new vw_WorkDays() { ID = 0, WorkDay = "الأحد" });
                }
                if (!model.IsMondaySelected)
                {
                    SelectedWorkDays.Add(new vw_WorkDays() { ID = 1, WorkDay = "الأثنين" });
                }
                if (!model.IsTuesdaySelected)
                {
                    SelectedWorkDays.Add(new vw_WorkDays() { ID = 2, WorkDay = "الثلاثاء" });
                }
                if (!model.IsWednesdaySelected)
                {
                    SelectedWorkDays.Add(new vw_WorkDays() { ID = 3, WorkDay = "الأربعاء" });
                }
                if (!model.IsThursdaySelected)
                {
                    SelectedWorkDays.Add(new vw_WorkDays() { ID = 4, WorkDay = "الخميس" });
                }

                if (SelectedWorkDays.Count < 5)
                    return CPartialView("_WeekDays",model).WithErrorMessages(JIC.Base.Resources.Messages.InValidMaxVacationNumber);
                   
                else if (workdayService.AddWorkDays(SelectedWorkDays))
                    return RedirectJS(Url.Action("Create")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                else
                    return CPartialView("_WeekDays", model).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);



            }
            return View();
        }
        private WorkingDaysViewModels GetWorkingDays()
        {
            List<vw_KeyValue> WorkDays = workdayService.GetWorkDays();
            List<DayOfWeek> WorkingDays = new List<DayOfWeek>();
            WorkingDaysViewModels model = new WorkingDaysViewModels();
            foreach (vw_KeyValue WD in WorkDays)
            {
                WorkingDays.Add((DayOfWeek)WD.ID);
            }
            if (WorkingDays != null && WorkingDays.Count > 0)
            {
                
                foreach (var day in WorkingDays)
                {
                    if (day == DayOfWeek.Friday)
                        model.IsFridaySelected = false;
                    if (day == DayOfWeek.Saturday)
                        model.IsSaturdaySelected = false;
                    if (day == DayOfWeek.Sunday)
                        model.IsSundaySelected = false;
                    if (day == DayOfWeek.Monday)
                        model.IsMondaySelected = false;
                    if (day == DayOfWeek.Tuesday)
                        model.IsTuesdaySelected = false;
                    if (day == DayOfWeek.Wednesday)
                        model.IsWednesdaySelected = false;
                    if (day == DayOfWeek.Thursday)
                        model.IsThursdaySelected = false;
                }
            }
            return model;
        }

        public ActionResult Days()
        {
            List<vw_KeyValue> WorkDays = workdayService.GetWorkDays();
            List<DayOfWeek> WorkingDays = new List<DayOfWeek>();
            WorkingDaysViewModels model = new WorkingDaysViewModels();
            foreach (vw_KeyValue WD in WorkDays)
            {
                WorkingDays.Add((DayOfWeek)WD.ID);
            }
            if (WorkingDays != null && WorkingDays.Count > 0)
            {
                
                foreach (var day in WorkingDays)
                {
                    if (day == DayOfWeek.Friday)
                        model.IsFridaySelected = false;
                    if (day == DayOfWeek.Saturday)
                        model.IsSaturdaySelected = false;
                    if (day == DayOfWeek.Sunday)
                        model.IsSundaySelected = false;
                    if (day == DayOfWeek.Monday)
                        model.IsMondaySelected = false;
                    if (day == DayOfWeek.Tuesday)
                        model.IsTuesdaySelected = false;
                    if (day == DayOfWeek.Wednesday)
                        model.IsWednesdaySelected = false;
                    if (day == DayOfWeek.Thursday)
                        model.IsThursdaySelected = false;
                }
                //return View(model);
            }
            return CPartialView("_WeekDays", model);
        }

    }
}