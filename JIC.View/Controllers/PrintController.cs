using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class PrintController : ControllerBase
    {
        // GET: Print
        public ActionResult Report(string ReportName,string ReportDesc,int? sessionId,int? caseId,int? printUser,int? rollId,int? CourtId)
        {
            var reportInf = new Models.ReportViewModel {
                 ReportName=ReportName,
                 ReportDesc=ReportDesc,
                  Url=string.Format("../../ReportForms/ReportForm.aspx?ReportName={0}&RollId={1}&SessionId={2}&CaseId={3}&PrintUser={4}&CourtID={5}",ReportName,rollId,sessionId,caseId, printUser, CourtId)
            };
            return CPartialView(reportInf);
        }

        public ActionResult ReportError()
        {
            return View();
        }
    }
}