using JIC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Helpers
{
    public class CAuthorize : AuthorizeAttribute
    {
        public CAuthorize(params SystemUserTypes[] userTypes)
        {
            this.Roles = String.Join(",", (from userType in userTypes select userType.ToString()));
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
                filterContext.HttpContext.Response.RedirectToRoute("UnAuthorized");
            else
                base.HandleUnauthorizedRequest(filterContext);
            
        }
    }
}