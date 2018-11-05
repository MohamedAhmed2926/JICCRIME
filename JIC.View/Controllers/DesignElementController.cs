using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class DesignElementController : Controller
    {
        // GET: DesignElement
        public ActionResult Index()
        {
            List<string> L = new List<string>
            {
                "abc",
                "xyz"
            };
            SelectListItem LI = new SelectListItem();
            var selectListItems = L.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
            ViewBag.ListOfEmails = selectListItems;
            return View();
        }
    }
}