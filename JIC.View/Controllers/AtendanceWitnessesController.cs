using JIC.Base;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.Secretary, SystemUserTypes.ElementaryCourtAdministrator, SystemUserTypes.Judge)]
    public class AtendanceWitnessesController : ControllerBase
    {
        private IWitnessesService WitnessesService;
        private ISessionService sessionService;

        // GET: AtendanceWitnesses
        public AtendanceWitnessesController(IWitnessesService WitnessesService, ISessionService sessionService)
        {
            this.WitnessesService = WitnessesService;
            this.sessionService = sessionService;

        }
        //[HttpGet]
        //public ActionResult Index(int caseID)
        //{

        //}
        //[HttpPost]
        //public ActionResult Index()
        //{

        //}
        public ActionResult Create(int CaseID,int SessionID,int WitnessID,int CircuitID)
        {
            WitnessSessionViewModel witness = new WitnessSessionViewModel();
            witness.CaseID = CaseID;
            witness.SessionID = SessionID;
            witness.WitnessID = WitnessID;
            witness.CircuitID = CircuitID;
            return View(witness);
        }
        [HttpPost]
        public ActionResult Create(WitnessSessionViewModel witness,HttpPostedFileBase file)
          {
            byte[] WitnessData = null;
            if (file != null && file.ContentLength > 0)
            {
                using (var binaryreader = new BinaryReader(file.InputStream))
                {
                    WitnessData = binaryreader.ReadBytes(file.ContentLength);
                }
            }
            var result = AddTestimonyStatus.FailedToAdd;
            if (sessionService.IsSessionSentToJudge(witness.SessionID))
            {
                result= AddTestimonyStatus.SentToJudge;
            }
            else
            {
                 result = WitnessesService.AddTestimonyToWitness(witness.CaseID, witness.SessionID, witness.WitnessTestimony, witness.WitnessID, WitnessData, SystemUserTypes.Secretary);
            }
                if (result == AddTestimonyStatus.AddedSuccessfully)
            {
               // return JavaScript("$(document).trigger('Documentwitness:Sucess');");
                return RedirectTo(Url.Action( "Index", "ALLMinutesOfSession", new {id=witness.CaseID,SessionID=witness.SessionID,CircuitID=witness.CircuitID })).WithErrorMessages("تمت العملية بنجاح");

                //  return CPartialView(witness).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

            }
            else if (result == AddTestimonyStatus.FailedToAdd)
            {
              //  return JavaScript("$(document).trigger('Documentwitness:Failed');");
                return RedirectTo(Url.Action( "Index", "ALLMinutesOfSession", new { id = witness.CaseID, SessionID = witness.SessionID, CircuitID = witness.CircuitID })).WithErrorMessages("لم تتم العملية بنجاح");

                //  return CPartialView(witness).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

            }
            else if (result == AddTestimonyStatus.SentToJudge)
            {
              //  return JavaScript("$(document).trigger('Sessionwitness:SentToJudge');");
                return RedirectTo(Url.Action( "Index", "ALLMinutesOfSession", new { id = witness.CaseID, SessionID = witness.SessionID, CircuitID = witness.CircuitID })).WithErrorMessages("عدم إمكانية إدخال الشهادة بعد إرسال المحضر للقاضي للتصديق");

                //  return CPartialView(witness).WithErrorMessages(JIC.Base.Resources.Resources.SendtoJudge);

            }
            return View(witness);
        }
    }
}