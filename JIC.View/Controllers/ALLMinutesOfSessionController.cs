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
    public class ALLMinutesOfSessionController : ControllerBase
    {
        private IDefectsService DefectsService;
        private ISessionService SessionService;
        private IDecisionService decisionService;
        public ALLMinutesOfSessionController(IDefectsService DefectsService, ISessionService SessionService, IDecisionService decisionService)
        {
            this.DefectsService = DefectsService;
            this.SessionService = SessionService;
            this.decisionService = decisionService;
        }
        // GET: ALLMinutesOfSession

        public ActionResult Index(int id, int SessionID, int CircuitID)
        {
             
                if (CurrentUser != null)
                {
                    vw_SessionData Session = SessionService.GetSessionData(SessionID);
                    MinutesOfSessionViewModel model = new MinutesOfSessionViewModel()
                    {
                        CaseID = id,
                        SessionID = SessionID,
                        CircuitID = CircuitID,
                        CourtID = CurrentUser.CourtID,
                        CurentUserID = CurrentUser.UserTypeID
                      ,
                        RollID = Session.RollID,
                    };
                    if (DefectsService.IsPresenceSaved(SessionID))
                    {

                        model.SaveAttendance = true;
                    }
                    else
                    {
                        model.SaveAttendance = false;
                    }

                    if (SessionService.IsSessionMinutesSaved(SessionID))
                    {
                        model.SaveMinutes = true;
                    }
                    else
                    {
                        model.SaveMinutes = false;
                    }
                    if (decisionService.IsDecisionSaved(id, SessionID))
                        model.SavedDecisions = true;
                    else
                        model.SavedDecisions = false;

                    if (SessionService.IsSessionSentToJudge(SessionID))
                        model.SentToJudge = true;
                    else
                        model.SentToJudge = false;


                    return View(model);
                }

                else
                {
                    return RedirectTo(Url.Action("login", "User",new {returnUrl ="/"})).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
                }
            }
        
    }
}