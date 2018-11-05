using JIC.Base;
using JIC.Base.Resources;
using JIC.Services.ServicesInterfaces;
using JIC.Crime.View.TestService;
using JIC.Crime.View.TestServiceI;
using JIC.Utilities.Helpers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using JIC.Crime.Entities.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections;
using JIC.Base.Entities.Models;
using JIC.Crime.View.TestInterfaces;
using JIC.Crime.Repositories;
using JIC.Base.Views;
using Microsoft.Owin.Security;
using JIC.Utilities.MvcHelpers;
using JIC.Services.Handler;
using JIC.Services.Services;

namespace JIC.Crime.View.Controllers
{
    public class ControllerBase : Controller
    {
        #region Messages

        public void ShowMessage(MessageTypes MessageType, string Message, bool AutoHide = true, Guid? ExceptionID = null)
        {
            ViewBag.Messages = new JIC.Base.Views.vw_MessageViewModel
            {
                MessageTypes = MessageType,
                Message = Message
            };
        }

        private void HandleException(Exception ex, bool ShowUserError = true)
        {
            Guid ExceptionID = LogHelper.LogException(ex, Request.Url.AbsoluteUri);
            Log.GetLogger().LogException(ex);
            if (!SystemConfigurations.Settings_InDevelopmentMode)
            {
                if (ShowUserError)
                    this.ShowMessage(MessageTypes.Error, Messages.ExceptionMessage, false, ExceptionID);
            }
            else
            {
                this.ShowMessage(MessageTypes.Error, LogHelper.GetExceptionInfo(ex), false);
            }
        }

        public ActionResult ErrorPage(Exception ex)
        {
            HandleException(ex);
            return RedirectTo(Url.Action("Index", "Error")).WithException(ex);
        }
        public CPartialView CPartialView()
        {
            return new CPartialView();
        }
        public CPartialView CPartialView(object Model)
        {
            return new CPartialView(Model);
        }
        public CPartialView CPartialView(string ViewName)
        {
            return new CPartialView(ViewName);
        }
        public CPartialView CPartialView(string ViewName, object Model)
        {
            return new CPartialView(ViewName, Model);
        }

        public CJavaScriptResult RedirectJS(string url)
        {
            return new CJavaScriptResult(url);
        }

        public CRedirectResult RedirectTo(string url)
        {
            return new CRedirectResult(url);
        }


        #endregion
        #region User
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private CustomRoleManager _roleManager = new CustomRoleManager(new Security_RoleStore(JIC.Crime.Repositories.DBInteractions.DBFactory.Get()));
        private static vw_UserData _user = null;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public CustomRoleManager RoleManager
        {
            get
            {
                return _roleManager;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private int GetCurrentUserID()
        {
            return User.Identity.GetUserId<int>();
        }
        public vw_UserData CurrentUser
        {
            get
            {
                return (IsAuthenticatied ? GetUser() : null);
            }
        }
        private vw_UserData GetUser()
        {
            if (_user == null || _user.ID != GetCurrentUserID())
                _user = ServiceUserService().FindByID(User.Identity.GetUserId<int>());
            return _user;
        }
        public bool IsAuthenticatied
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }
        #endregion
        #region Errors
        public Hashtable ModelErrors
        {
            get
            {
                var errors = new Hashtable();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).First();
                    }
                }
                return errors;
            }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            HandleException(filterContext.Exception);

            //base.OnException(filterContext);
            if (!filterContext.ExceptionHandled)
            {
                //Mark as Exception Logged
                filterContext.ExceptionHandled = true;

                TempData["ExceptionMessage"] = filterContext.Exception;
                filterContext.Result = RedirectToAction("Index", "Error");
            }
        }
        #endregion
        #region Services



        public Services.UserService ServiceUserService()
        {
            return new Services.UserService(UserManager, SignInManager, User, UnityConfig.GetService<IUserService>());
        }
        #endregion

        #region Database
        public JICCrimeContext DataContext
        {
            get
            {
                return JIC.Crime.Repositories.DBInteractions.DBFactory.Get();
            }
        }
        #endregion

    }
}


