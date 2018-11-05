using JIC.Base;

using JIC.Services.ServicesInterfaces;
using JIC.Base.views;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{


    //[CAuthorize( SystemUserTypes.CourtHead , SystemUserTypes.InitialCourtAdministrator , SystemUserTypes.ElementaryCourtAdministrator,
    //   SystemUserTypes.CriminalDepManager , SystemUserTypes.ImplementationEmployee , SystemUserTypes.InquiriesEmployee , SystemUserTypes.JICAdmin , SystemUserTypes.Judge )]
    [CAuthorize(SystemUserTypes.ElementaryCourtAdministrator)]
    public class VacationsController : ControllerBase
    {
        public IVacationService VacationService;
        public IWorkdayService WorkDaysService;

     
        public VacationsController(IVacationService VacationService, IWorkdayService WorkDaysService)
        {
            this.VacationService = VacationService;
            this.WorkDaysService = WorkDaysService;
        }
        public SaveStatus AddVacation(VacationsModel vacationData,out int vacationID)
        {

            vw_VacationData VD = new vw_VacationData();
            VD.ID = vacationData.ID;
            VD.VacationName = vacationData.Name;
            VD.VacationFrom = vacationData.FromDate;
            VD.VacationTo = vacationData.EndDate;
            //int vacationID;
           return VacationService.AddVacation(VD , out vacationID);
        }

        

        public SaveStatus EditVacation(VacationsModel vacationData)
        {

            vw_VacationData VD = new vw_VacationData();
            VD.ID = vacationData.ID;
            VD.VacationName = vacationData.Name;
            VD.VacationFrom = vacationData.FromDate;
            VD.VacationTo = vacationData.EndDate;

            return VacationService.EditVacation(VD);
        }

        public List<VacationsModel> GetVacations()
        {
            List<vw_VacationData> L = VacationService.GetVacations();
            List<VacationsModel> VM = new List<VacationsModel>();
            foreach (vw_VacationData o in L)
            {
                VM.Add(new VacationsModel { FromDate = o.VacationFrom, EndDate = o.VacationTo, Name = o.VacationName, ID = o.ID });
            }



            return VM;
        }

        DeleteStatus  DeleteVacation(int ID)
        {

            return VacationService.DeleteVacation(ID);
        }

        List<DayOfWeek> GetWeekEndVacationDays()
        {
            List<DayOfWeek> AllWeekDays = new List<DayOfWeek> { DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday };
            List<vw_KeyValue> WorkDays=  WorkDaysService.GetWorkDays();
            List<DayOfWeek> WorkDays1 = new List<DayOfWeek>();
            foreach (vw_KeyValue WD in WorkDays)
            {
                WorkDays1.Add((DayOfWeek)WD.ID);
            }
            List<DayOfWeek> nonintersect = AllWeekDays.Except(WorkDays1).ToList();
            return nonintersect;
        }
        //=====================================================================




        // GET: Vacations
        public ActionResult Index()
        {
            List<DayOfWeek> WeekEnds= GetWeekEndVacationDays();
            string WEnds = "";
            foreach (DayOfWeek L in WeekEnds)
                {
                WEnds = WEnds+ "," + ((int)L).ToString() ;
            }

            string resultStr = string.Join(string.Empty, WEnds.Skip(1));
            ViewData["WeekEndDays"] = resultStr;

             VacationsModel VM=new VacationsModel() ;
            VM.FromDate = DateTime.Now.AddDays(1);
            VM.EndDate = DateTime.Now.AddDays(1);
          
            return View(VM);
        }

        [ValidateAntiForgeryToken ]
        [HttpPost]
        public ActionResult Index(VacationsModel VacationsObject)
        {
            ViewData["SBeforeID"] = false;
         
            int VaCationID = 0;

            if (VacationsObject.FromDate > VacationsObject.EndDate )
            {
                ModelState.AddModelError("EndDate", Base.Resources.Messages.Durations );
            }
            if (VacationsObject.FromDate < DateTime.Now)
            {
                return RedirectTo(Url.Action("Index")).WithErrorMessages("تاريخ بدايةالأجازة يجب ان يكون أكبر من تاريخ اليوم");

              
            }

            if (ModelState.IsValid)
            {
                SaveStatus SavingStatus = AddVacation(VacationsObject,out VaCationID );
                if (SavingStatus == SaveStatus.Failed_To_Save)
                {
                    ShowMessage(MessageTypes.Error, "لم يتم الحفظ");
                }
                else if (SavingStatus == SaveStatus.Saved_Before)
                {
                    ViewData["SBeforeID"] = true;
                    VacationsObject.ID = VaCationID;
                    ShowMessage(MessageTypes.Error, "تم حفظ الاجازة من قبل");
                }
             else   if (SavingStatus == SaveStatus.WorkingDay)
                {
                    ShowMessage(MessageTypes.Error, "لا يمكن حفظ اجازة... يوجد جلسات خلال هذه الفترة");
                }
                
                  
               
                else if (SavingStatus == SaveStatus.Saved)
                {
                    return RedirectTo(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                }
            }
            ViewData["VacID"] = VaCationID;
            return View(VacationsObject);
        }
        public ActionResult Edit(int ID)
        {
            var Vacation = VacationService.GetVacations().Where(vacation => vacation.ID == ID).First();
            VacationsModel VM = new VacationsModel
            {
                ID = ID,
                Name = Vacation.VacationName,
                FromDate = Vacation.VacationFrom,
                EndDate = Vacation.VacationTo
            };
            ViewData["VID"] = ID;
            return View(VM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(VacationsModel VacationObj)
        {
            ViewData["SBeforeID"] = false;

            if (VacationObj.FromDate > VacationObj.EndDate)
            {
                ModelState.AddModelError("EndDate", "تاريخ انتهاء الاجازة يجب ان يكون اكبر من او يساوى تاريخ البدايه");
            }
            if (ModelState.IsValid)
            {
                SaveStatus SavingStatus = EditVacation(VacationObj);
                if (SavingStatus == SaveStatus.Failed_To_Save)
                {
                    ShowMessage(MessageTypes.Error, "لم يتم الحفظ");
                }
                //else if (SavingStatus == SaveStatus.Saved_Before)
                //{
                //    ViewData["SBeforeID"] = true;
                //    ModelState.AddModelError("", "تم حفظ الاجازة من قبل");
                //}
                if (SavingStatus == SaveStatus.WorkingDay)
                {
                    ShowMessage(MessageTypes.Error, "لا يمكن حفظ اجازة فى يوم عمل");
                }
                else if (SavingStatus == SaveStatus.Saved)
                {
                    ShowMessage(MessageTypes.Success,JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                }
            }

            return View(VacationObj);

           
        }

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public JsonResult  Delete(int VacationID)
        //{
        //  DeleteStatus  ReturnVal=  DeleteVacation (VacationID);

        //    return Json((int)ReturnVal ,JsonRequestBehavior.AllowGet );
        //}

        [HttpGet]
        public ActionResult Delete(int vacationID)
        {
            try
            {
                var Vacation = VacationService.GetVacations().Where(vacation => vacation.ID == vacationID).First();
                VacationsModel VM = new VacationsModel
                {
                    ID = vacationID,
                    Name = Vacation.VacationName,
                    FromDate = Vacation.VacationFrom,
                    EndDate = Vacation.VacationTo
                };
               
                return CPartialView(VM);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(VacationsModel model)
        {
            try
            {
                DeleteStatus ReturnVal = DeleteVacation(model.ID);
                if (ReturnVal == DeleteStatus.Deleted)
                    return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                else
                    return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
               
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }

        DateTime GetDateFromString(String Date_)
            {
            DateTime D;
            D = DateTime.ParseExact(Date_, "MM/dd/yyyy",null);


            return D;

        }
            
            // set: Vacations
        public ActionResult CalendarPV()
        {
           
           

                return PartialView("CalendarPV");
         
        }

        [HttpGet]
        public JsonResult GetListOfVacations()
        {
            List<VacationsModel> L = GetVacations();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
  
     
    }
}