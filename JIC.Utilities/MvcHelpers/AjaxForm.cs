using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Utilities.MvcHelpers
{
    public class AjaxForm : IDisposable
    {
        private HtmlHelper HtmlHelper;
        public AjaxForm(HtmlHelper HtmlHelper,string urlName, string ID)
        {
            this.HtmlHelper = HtmlHelper;
            FormOpen(urlName,ID);
        }
        private void FormOpen(string Url,string ID)
        {
            HtmlHelper.ViewContext.Writer.WriteLine(String.Format(@"<div id=""{0}"">",ID));
            HtmlHelper.ViewContext.Writer.WriteLine(String.Format(@"<form action=""{0}"" data-ajax=""true"" data-ajax-method=""POST"" data-ajax-mode=""replace"" data-ajax-update=""#{1}"" id=""{1}"" method=""post"" novalidate=""novalidate"">",Url,ID));
            HtmlHelper.ViewContext.Writer.WriteLine(HtmlHelper.AntiForgeryToken());
        }
        private void FormClose()
        {
            HtmlHelper.ViewContext.Writer.WriteLine("</form></div>");
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    FormClose();
                }
                disposedValue = true;
            }
        }

      
        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}