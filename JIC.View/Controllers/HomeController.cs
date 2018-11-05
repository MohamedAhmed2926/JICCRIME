using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
     
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult DesignElement()
        {
            List<vw_UserData> persons = new List<vw_UserData> {
                new vw_UserData {
                ID = 1,
                UserName = "Ahmed",
                CourtID = 1
                },
                new vw_UserData {
                ID = 2,
                UserName = "Ahmed",
                CourtID = 1
                },
                new vw_UserData {
                ID = 3,
                UserName = "Ahmed",
                CourtID = 1
                },
                new vw_UserData {
                ID = 4,
                UserName = "Ahmed",
                CourtID = 1
                }
            }; 
            


            return View(persons.AsEnumerable());
        }
    }
}