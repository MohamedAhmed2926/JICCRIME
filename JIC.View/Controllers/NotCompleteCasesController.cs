using JIC.Base;
using JIC.Services.ServicesInterfaces;
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
    [CAuthorize(SystemUserTypes.schedualEmployee, SystemUserTypes.ElementaryCourtAdministrator)]

    public class NotCompleteCasesController : ControllerBase
    {
        private INotCompleteCasesService NotCompleteCasesService;
        private ICrimeCaseService CaseService;
        public NotCompleteCasesController(INotCompleteCasesService NotCompleteCasesService, ICrimeCaseService caseService)
        {
            this.NotCompleteCasesService = NotCompleteCasesService;
            this.CaseService = caseService;
        }
        // GET: NotCompleteCase
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                try
            {
                List<NotCompleteCaseViewModels> NotCompleteCase = NotCompleteCasesService.GetNotCompleteCase(IsAuthenticatied ? CurrentUser.CourtID.Value : 0).ToList()
               .Select(Case => new NotCompleteCaseViewModels
               {
                   CaseId = Case.CaseId,
                   CaseTitle = Case.CaseTitle,
                   CrimName = Case.CrimName,
                   FirstNumber = Case.FirstNumber + " / " + Case.FirstYear + " / " + Case.FirstprosecutionName,
                   SecondNumber = Case.SecondNumber + " / " + Case.SecondYear + " / " + Case.SecondprosecutionName,
                   NotCompleteStatus = Case.NotCompleteStatus,
               }).ToList();

                foreach (var Case in NotCompleteCase)
                {
                    int countStatus = 0; 
                    int count = Case.NotCompleteStatus.Count();
                    foreach (var CaseStatus in Case.NotCompleteStatus)
                    {
                        switch (CaseStatus)
                        {
                            case NotCompleteStatus.Defendent:
                                Case.ShowCaseStatus = JIC.Base.Resources.Resources.CaseParties;
                                countStatus++;
                                break;
                            case NotCompleteStatus.Document:
                                Case.ShowCaseStatus += JIC.Base.Resources.Resources.CaseDocuments;
                                countStatus++;
                                break;
                            case NotCompleteStatus.OrderOfAssignment:
                                Case.ShowCaseStatus +=JIC.Base.Resources.Resources.OrderDescription;
                                countStatus++;
                                break;
                            case NotCompleteStatus.OverAllNumber:
                                Case.ShowCaseStatus +=JIC.Base.Resources.Resources.OverAllNumber;
                                countStatus++;
                                break;

                        }
                        if (countStatus != count)
                        {
                            Case.ShowCaseStatus += "/";
                        }
                    }
                }


                return View(NotCompleteCase);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }

                else
                {
                    return RedirectTo(Url.Action("login", "User",new {returnUrl ="/"})).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
}
        }
       
        public ActionResult Complete(int id)
        {
            if (CurrentUser != null)
            {
                try
            {
                List<NotCompleteCaseViewModels> NotCompleteCase = NotCompleteCasesService.GetNotCompleteCase(IsAuthenticatied ? CurrentUser.CourtID.Value : 0).Where(e=>e.CaseId==id).ToList()
               .Select(Case => new NotCompleteCaseViewModels
               {
                   CaseId = Case.CaseId,
                   CaseTitle = Case.CaseTitle,
                   CrimName = Case.CrimName,
                   FirstNumber = Case.FirstNumber + " / " + Case.FirstYear + " / " + Case.FirstprosecutionName,
                   SecondNumber = Case.SecondNumber + " / " + Case.SecondYear + " / " + Case.SecondprosecutionName,
                   NotCompleteStatus = Case.NotCompleteStatus,
               }).ToList();
                foreach (var CaseStatus in NotCompleteCase.Single().NotCompleteStatus)
                    {
                        switch (CaseStatus)
                        {
                        case NotCompleteStatus.Defendent:
                                return RedirectToAction("Index", "CaseParties", new { CaseID = id });
                        case NotCompleteStatus.OrderOfAssignment:
                            return RedirectToAction("Create", "OrderOfAssignment", new { id = id });

                        case NotCompleteStatus.Document:
                                return RedirectToAction("Index", "Attachment", new { id = id });
                        case NotCompleteStatus.OverAllNumber:
                                return RedirectToAction("Index", "OverAllNumber", new { CaseID = id });
                                
                        }
                    }

                return RedirectToAction("Edit", "Case", new { id = id });

            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var unCompletCase = CaseService.GetCaseBasicData(id);
                NotCompleteCaseViewModels NotCompleteCaseView = new NotCompleteCaseViewModels()
                {
                    CaseId = unCompletCase.CaseID,
                    CaseTitle = unCompletCase.CaseName,
                    CrimName = unCompletCase.CrimeTypeName,
                    FirstNumber = unCompletCase.FirstNumberInt + " / " + unCompletCase.FirstYearInt + " / " + unCompletCase.FirstprosecutionName,
                    SecondNumber = unCompletCase.SecondNumberInt + " / " + unCompletCase.SecondYearInt + " / " + unCompletCase.SecoundProsecutionName,

                };
                return PartialView(NotCompleteCaseView);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(NotCompleteCaseViewModels NotCompleteCase)
        {
            try
            {
                if (NotCompleteCasesService.DeleteNotCompleteCase(NotCompleteCase.CaseId))
                {
                    return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                }
                else
                {
                    return CPartialView(NotCompleteCase).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                }
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }

        }


    }
}