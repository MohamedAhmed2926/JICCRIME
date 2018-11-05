using JIC.Base;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class OverAllNumberController : ControllerBase
    {
        private IOverAllNumberService overAllNumberService;
        public OverAllNumberController(IOverAllNumberService overAllNumberService)
        {
            this.overAllNumberService = overAllNumberService;
        }
        // GET: OverAllNumber
        public ActionResult Index(int CaseID)
        {
            if(CurrentUser != null)
            { return View(PrepareViewModel(CaseID)); }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(OverAllNumberViewModel model)
        {
            if (CurrentUser != null)
            {
                List<AddOverAllStatus> Errors;
                List<string> Messages = new List<string>();
                long number;
                int ProsecutionNumber, Year;
                var _AddOverAllStatus = overAllNumberService.AddOverAllNumber(model.CaseID, out number, out ProsecutionNumber, out Year, out Errors);
                if (_AddOverAllStatus == AddOverAllStatus.Error)
                {
                    if (Errors.Count != 0 || Errors != null)
                    {
                        foreach (var error in Errors)
                        {
                            if (error == AddOverAllStatus.Defendent)
                                Messages.Add(JIC.Base.Resources.Messages.UnCompletedCaseDefendent);
                            else if (error == AddOverAllStatus.Document)
                                Messages.Add(JIC.Base.Resources.Messages.UnCompletedCaseDocument);
                            else if (error == AddOverAllStatus.Obtainment)
                                Messages.Add(JIC.Base.Resources.Messages.UnCompletedCaseOptaimentDocument);
                            else if (error == AddOverAllStatus.OverAllReserved)
                                Messages.Add(JIC.Base.Resources.Messages.UnCompletedCaseOverAllReserved);
                            else if (error == AddOverAllStatus.Fail)
                                Messages.Add(JIC.Base.Resources.Messages.OperationNotCompleted);
                            else if (error == AddOverAllStatus.Description)
                                Messages.Add(JIC.Base.Resources.Messages.UnCompletedCaseDescription);
                        }
                    }
                }

                switch (_AddOverAllStatus)
                {
                    case AddOverAllStatus.Error:
                        return RedirectTo(Url.Action("Index", new { CaseID = model.CaseID })).WithErrorMessages(Messages.ToArray());
                    case AddOverAllStatus.Saved:
                        return RedirectTo(Url.Action("Search", "SearchCase")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                    case AddOverAllStatus.OverAllReserved:
                        return RedirectTo(Url.Action("Index", new { CaseID = model.CaseID })).WithErrorMessages(JIC.Base.Resources.Messages.UnCompletedCaseOverAllReserved);
                        //case AddOverAllStatus.Defendent:
                        //    return RedirectTo(Url.Action("Index", new { CaseID = model.CaseID })).WithErrorMessages(JIC.Base.Resources.Messages.UnCompletedCaseDefendent);
                        //case AddOverAllStatus.Document:
                        //    return RedirectTo(Url.Action("Index" ,new { CaseID = model.CaseID })).WithErrorMessages(JIC.Base.Resources.Messages.UnCompletedCaseDocument);

                        //case AddOverAllStatus.Fail:
                        //    return RedirectTo(Url.Action("Index", new { CaseID = model.CaseID })).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                        //case AddOverAllStatus.OverAllReserved:
                        //    return RedirectTo(Url.Action("Index", new { CaseID = model.CaseID })).WithErrorMessages(JIC.Base.Resources.Messages.UnCompletedCaseOverAllReserved);
                        //case AddOverAllStatus.Saved:
                        //    return RedirectTo(Url.Action("Index", new { CaseID = model.CaseID })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                }

                return View();
            }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");

            }
        }
        #region Helpers
        private OverAllNumberViewModel PrepareViewModel(int CaseID)
        {
            return new OverAllNumberViewModel
            {
                CaseID = CaseID
            };
        }
        #endregion
    }
}