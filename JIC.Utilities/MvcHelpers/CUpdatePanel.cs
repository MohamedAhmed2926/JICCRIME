using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Utilities.MvcHelpers
{
    public enum Method
    {
        Get,
        Post
    }
    public class CUpdatePanel : IDisposable
    {
        private ViewContext viewContext = null;
        private string ID;
        private Method Method;

        public CUpdatePanel(ViewContext viewContext, string ID,string Url,Method Method = Method.Get)
        {
            this.viewContext = viewContext;
            this.ID = ID;
            viewContext.Writer.Write(String.Format("<div id='{0}' data-url='{1}'>", ID,Url));
            this.Method = Method;
        }
        public void EndPanel()
        {
            viewContext.Writer.Write(@"
                 </div>
                <script>
                    OnDocReady(function () {
                        $('#" + ID + @"').on('update',function(event,parameters){
                            event.stopPropagation();
                             var output = $('#" + ID + @" :input').serializeArray(); 
                            //$.map($('#" + ID+ @" :input').serializeArray(), function( val, i ) { output[val.name] = val.value; } );
                           $('#" + ID + @"').html(""<div class='row text-center'><img src='" + UrlHelper.GenerateContentUrl("/Content/img/loading.gif", viewContext.HttpContext) + @"' alt='Loading' /></div>"");");
            switch (Method)
            {
                case Method.Get:
                    viewContext.Writer.Write(@"
                            $.get( $(this).data('url'), parameters)
                                .done(function( data ) {
                                    $('#" + ID + @"').html(data);
                                    $('#" + ID + @"').trigger('update:done');
                                });");
                    break;
                case Method.Post:
                    viewContext.Writer.Write(@"
                            $.post( $(this).data('url')+'?'+$.param(parameters),output )
                                .done(function( data ) {
                                    $('#" + ID + @"').html(data);
                                    $('#" + ID + @"').trigger('update:done');
                                });");
                    break;
            }
            viewContext.Writer.Write(@"
                        });
                    });
                </script>
            ");
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    EndPanel();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}