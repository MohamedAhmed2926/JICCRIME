using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class CaseWitnessController : Controller
    {
        // GET: CaseWitness
        public ActionResult Index(int id)
        {
            List<CaseWitnessesViewModel> caseWitness = new List<CaseWitnessesViewModel>();
            return View(caseWitness);
        }
    }
}