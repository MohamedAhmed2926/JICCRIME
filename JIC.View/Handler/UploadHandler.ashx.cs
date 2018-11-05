using JIC.Base;
using JIC.Services.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace JIC.Crime.View.Handler
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HttpPostedFile file = context.Request.Files["Filedata"];
                var id = context.Request["id"];
                string vpath = SystemConfigurations.Settings_TempFilesFolderPath + Guid.NewGuid().ToString().Replace("-", "");
                string filePath = context.Server.MapPath(vpath);
                file.SaveAs(filePath);
                context.Response.ContentType = "application/json";
                var json = new JavaScriptSerializer().Serialize(new {
                    filePath,
                    vpath,
                    id,
                    file.FileName
                });

                context.Response.Write(json);
            }
            catch (Exception ex)
            {
                Log.GetLogger().LogException(ex);
                context.Response.StatusCode = 404;
                context.Response.Write("error|" + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}