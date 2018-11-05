using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JIC.Utilities.MvcHelpers
{
    public class CPartialView : PartialViewResult
    {
        private List<string> SuccessMessages = new List<string>();
        private List<string> ErrorMessages = new List<string>();
        private string prefix = "";
        public CPartialView()
        {

        }
        public CPartialView(object model)
        {
            this.ViewData.Model = model;
        }
        public CPartialView(string ViewName)
        {
            this.ViewName = ViewName;
        }
        public CPartialView(string ViewName, object model)
        {
            this.ViewName = ViewName;
            this.ViewData.Model = model;
        }
        public CPartialView WithSuccessMessages(params string[] messages)
        {
            SuccessMessages.AddRange(messages);
            return this;
        }
        public CPartialView WithPrefix(string prefix)
        {
            this.prefix = prefix;
            return this;
        }
        public CPartialView WithErrorMessages(params string[] messages)
        {
            ErrorMessages.AddRange(messages);
            return this;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.ViewData.Model = this.ViewData.Model;
            this.ViewData = context.Controller.ViewData;
            this.TempData = context.Controller.TempData;

            if (!String.IsNullOrEmpty(prefix))
            {
                ViewData.TemplateInfo.HtmlFieldPrefix = prefix;
            }
            base.ExecuteResult(context);

            foreach (var message in SuccessMessages)
            {
                context.HttpContext.Response.Output.WriteLine(@"
                    <script>
                        if (window.jQuery) {  
                            $(document).trigger('SuccessMessage',['" + message + @"']);
                        }else{
                            document.addEventListener('load', function(){ $(document).trigger('SuccessMessage',['" + message + @"']); } );
                        }
                        
                    </script>");
            }
            foreach (var message in ErrorMessages)
            {
                context.HttpContext.Response.Output.WriteLine(@"
                    <script>
                        if (window.jQuery) {  
                            $(document).trigger('ErrorMessage',['" + message + @"']);
                        }else{
                            document.addEventListener('load', function(){ $(document).trigger('ErrorMessage',['" + message + @"']); } );
                        }
                        
                    </script>");
            }
        }
    }
}