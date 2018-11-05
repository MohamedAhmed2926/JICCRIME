using JIC.Base;
using JIC.Base.Resources;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class ApproveByJudgeController : ControllerBase
    {
        // GET: ApproveByJudge
        private ISessionService SessionService;
        private IRollService rollservice;
        public ApproveByJudgeController( ISessionService SessionService, IRollService rollservice)
        {
            this.rollservice = rollservice;
            this.SessionService = SessionService;
        }
        public ActionResult Index()
        {
            if(CurrentUser !=null)
            { 
            ApproveSessionsViewModel model= new ApproveSessionsViewModel();
            model.Sessions = SessionService.GetSentToJudgeSessionDates(CurrentUser.ID);
            
   
            return View(model);
        }

                else
                {
                    return RedirectTo(Url.Action("login", "User",new {returnUrl ="/"})).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
}
        }

        [HttpPost]
        public ActionResult Index(ApproveSessionsViewModel model)
        {
            if(CurrentUser !=null)
            { 
         
            model.Sessions = SessionService.GetSentToJudgeSessionDates(CurrentUser.ID);


            return View(model);
        }

                else
                {
                    return RedirectTo(Url.Action("login", "User",new {returnUrl ="/"})).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
}
        }

        [HttpGet]
        public ActionResult GetCases(int RollID)
        {
          
             // ApproveSessionsViewModel model = new ApproveSessionsViewModel();
           List< ApproveSessionsViewModel> CasesList = new List<ApproveSessionsViewModel>();
            // rollservice.
            if (CurrentUser != null)
            {
                List<vw_RollCases> CasesInSession = SessionService.GetSessionsToApprove(CurrentUser.ID,RollID );
            ViewData["UserID"] = CurrentUser.ID;
            ViewData["CourtID"] = CurrentUser.CourtID;
            foreach (var r in CasesInSession)
            {
                CasesList.Add(new ApproveSessionsViewModel
                {
                    FirstLevelNumber = r.FirstLevelNumber,
                    SecondLevelNumber = r.SecondLevelNumber,
                    CaseID = r.CaseID,
                    SessionID = r.SessionID,
                    RollDate = r.RollDate.ToShortDateString(),
                    Title=r.Title,
                    OverAllNumber=r.OverAllNumber,
                    MainCrime=r.MainCrime,
                    CircuitID=r.CircuitID,
                    RollID=RollID,
                    
                });

            }
                ViewData["SessionEnded"] = false;
            return PartialView(CasesList);
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView(CasesList);
            }
        }

      
        public ActionResult Approve(int SessionID, int CaseID)
        {
            ApproveSessionsViewModel model = new ApproveSessionsViewModel();
            model.SessionID = SessionID;
            model.CaseID = CaseID;
            return CPartialView(model);

        }
        [HttpPost]
        public ActionResult Approve(ApproveSessionsViewModel model)
        {
            ApproveByJudgeStatus result =SessionService.ApproveSessionByJudge((int)model.SessionID, model.CaseID);
            switch (result)
            {
                case ApproveByJudgeStatus.Failed:
                    return RedirectJS(Url.Action("Index")).WithErrorMessages(Messages.OperationNotCompleted);
                case ApproveByJudgeStatus.Failed_Attachements:
                    return RedirectJS(Url.Action("Index")).WithErrorMessages(Messages.AttachmentsUnSaved);
                case ApproveByJudgeStatus.Failed_Decision:
                    return RedirectJS(Url.Action("Index")).WithErrorMessages(Messages.DecisionUnSaved);
                case ApproveByJudgeStatus.Failed_DefectsPresence:
                    return RedirectJS(Url.Action("Index")).WithErrorMessages(Messages.DefectsPresenceUnSaved);
                case ApproveByJudgeStatus.Failed_Session:
                    return RedirectJS(Url.Action("Index")).WithErrorMessages(Messages.SessionMinUnSaved);
                case ApproveByJudgeStatus.SessionApprovedSuccessfully:
                    // return CPartialView().WithSuccessMessages(Messages.OperationCompletedSuccessfully);
                     return RedirectJS (Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                   // return Json(Messages.OperationCompletedSuccessfully, JsonRequestBehavior.AllowGet);
            }
            return RedirectJS(Url.Action("Index"));

        }

        public ActionResult ReturnToSecretary(int SessionID)
        {
            ApproveSessionsViewModel model = new ApproveSessionsViewModel();
            model.SessionID = SessionID;
            return CPartialView(model);

        }

        [HttpPost]
        public ActionResult ReturnToSecretary(ApproveSessionsViewModel model)
        {
            try
            {
                SessionService.ReturnSessionToSecretary((int)model.SessionID);
                return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
            }
            catch (Exception)
            {
                return RedirectJS(Url.Action("Index")).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
            }
      

        }
    }
}