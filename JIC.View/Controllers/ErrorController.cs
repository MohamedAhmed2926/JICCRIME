using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class ErrorController : ControllerBase
    {
        // GET: Error
        public ActionResult Index()
        {
            ErrorViewModel errorViewModel = new ErrorViewModel
            {
                ErrorTitle = JIC.Base.Resources.Resources.Error,
                ErrorDetail = JIC.Base.Resources.Resources.Error
            };
            if (TempData["ExceptionMessage"] != null)
            {
                Exception exception = (Exception)TempData["ExceptionMessage"];
                errorViewModel.ErrorTitle = exception.Message;
                errorViewModel.ErrorDetail = exception.StackTrace;
            }
            return View(errorViewModel);
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}