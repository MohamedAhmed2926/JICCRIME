using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Utilities.MvcHelpers
{
    public class CModel : IDisposable
    {
        private bool IsDisposed = false;
        private ViewContext viewContext = null;
        public CModel(ViewContext viewContext,string ID,string ModelTitle)
        {
            this.viewContext = viewContext;
            viewContext.Writer.Write(" <div id='" + ID + @"' class='modal fade' role='dialog'>
                <div class='modal-dialog'>
                <!-- Modal content-->
                <div class=""modal-content"">
                  <div class=""modal-header"">
                    <button type = ""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                    <h4 class=""modal-title"">" + ModelTitle + @"</h4>
                  </div>
                  <div class=""modal-body"">");
        }
        public void EndModel()
        {
            viewContext.Writer.Write(@"
                 </div>
                </div>
              </div>
            </div>");
            IsDisposed = true;
        }
        public void Dispose()
        {
            if(!IsDisposed)
                EndModel();
        }
    }
}