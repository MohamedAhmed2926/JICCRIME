using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Helpers
{
    public static class MVCHelpers
    {
        public static void CRenderAction(this HtmlHelper helper,string Action,string Controller,params object[] Param)
        {
            var ControllerType = Assembly.GetAssembly(typeof(JIC.Crime.View.Controllers.ControllerBase)).GetTypes().Where(type => type.Namespace != null && type.Namespace.Equals("JIC.Crime.View.Controllers") && type.Name == Controller).FirstOrDefault();
            if (ControllerType == null)
                throw new Exception("Controller Not Found");
            
            object obj = UnityConfig.GetObject(ControllerType);
            if (obj == null)
                throw new Exception("Couldn't Initialize Object");

            var ActionMethod = ControllerType.GetMethod(Action);
            if (ActionMethod == null)
                throw new Exception("Action Not Found in Controller");
            ActionResult result =  (ActionResult) ActionMethod.Invoke(obj, Param);
            result.ExecuteResult(helper.ViewContext);
        }
    }
}