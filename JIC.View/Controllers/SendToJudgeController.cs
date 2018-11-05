using JIC.Base;
using JIC.Base.Resources;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class SendToJudgeController : ControllerBase
    {
        private ISessionService SessionSendService;
        public SendToJudgeController(ISessionService SessionSendService)
        {
            this.SessionSendService = SessionSendService;
        }

        // GET: SendToJudge

        public ActionResult SendPartial(int id, int SessionID)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                JudgeViewModel model = new JudgeViewModel();
                model.SessionID = SessionID;
                model.CaseID = id;
                return PartialView(model);
            }
            else
            {

                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendPartial(JudgeViewModel model)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                if (SessionSendService.IsSessionSentToJudge(model.SessionID))
                {
                    return CPartialView(model).WithErrorMessages(JIC.Base.Resources.Messages.SentBefore);
                }

                else
                {
                    ApproveByJudgeStatus _SendingStatus = SessionSendService.SendSessionToJudge(model.SessionID, model.CaseID);
                    switch (_SendingStatus)
                    {
                        case ApproveByJudgeStatus.Failed:
                            return CPartialView(model).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                        case ApproveByJudgeStatus.Failed_Attachements:
                            return CPartialView(model).WithErrorMessages(JIC.Base.Resources.Messages.AttachmentsUnSaved);
                        case ApproveByJudgeStatus.Failed_Decision:
                            return CPartialView(model).WithErrorMessages(JIC.Base.Resources.Messages.DecisionUnSaved);
                        case ApproveByJudgeStatus.Failed_DefectsPresence:
                            return CPartialView(model).WithErrorMessages(JIC.Base.Resources.Messages.DefectsPresenceUnSaved);
                        case ApproveByJudgeStatus.Failed_Session:
                            return CPartialView(model).WithErrorMessages(JIC.Base.Resources.Messages.SessionMinUnSaved);
                        case ApproveByJudgeStatus.SessionSentSuccessfully:
                            ViewBag.Saved = true;
                            return CPartialView(model).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

                    }
                }
                return View(model);
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }
    }
}