using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class WitnessesAttendanceController : Controller
    {
        // GET: WitnessesAttendance
        public ActionResult Index(int id , int SessionID, int CircuitID )
        {
            MinutesOfSessionViewModel MinutesOfSession = new MinutesOfSessionViewModel();
            MinutesOfSession.CaseID = id;
            MinutesOfSession.SessionID = SessionID;
            MinutesOfSession.CircuitID = CircuitID;
            return View(MinutesOfSession);
        }

        [HttpPost]
        public ActionResult Index(MinutesOfSessionViewModel Model)
        {
            return View();
        }
    }
}