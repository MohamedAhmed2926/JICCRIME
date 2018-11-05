using JIC.Base;
using JIC.Base.Views;

using JIC.Services.Services;
using JIC.Services.ServicesInterfaces;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIC.Crime.View.ReportForms
{
    public partial class ReportForm : System.Web.UI.Page
    {
        ICircuitService circuitService = new CourtConfigurationService(CaseType.Crime);
        ISessionService sessionServ = new JIC.Services.Services.SessionsService();
       // ICaseConfiguration caseConfiguration = new CaseSessionsService(CaseType.Crime);
        // LookUpService LookUpService = new LookUpService(CaseType.Crime);
        public SystemReports CurrentReport
        { get {
                SystemReports reportName = GetRepNameType(Request.QueryString["ReportName"].ToString());
              

                return reportName;
            } }

        public int PrintUser { get { return Request.QueryString["PrintUser"] != null ? int.Parse(Request.QueryString["PrintUser"]) : 0; } }
        public int CourtID { get { return Request.QueryString["CourtID"] != null ? int.Parse(Request.QueryString["CourtID"]) : 0; } }
        public int RollId { get { return Request.QueryString["RollId"] != null ? int.Parse(Request.QueryString["RollId"]) : 0; } }

        public int SessionId { get { return Request.QueryString["SessionId"] != null ? int.Parse(Request.QueryString["SessionId"]) : 0; } }
        public int CaseId { get { return Request.QueryString["CaseId"] != null ? int.Parse(Request.QueryString["CaseId"]) : 0; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadControl();
            }
            

        }
        private Base.SystemReports GetRepNameType(string repName)

        {
            Base.SystemReports itype;
            if (!Enum.TryParse(repName, out itype))
            {
                throw new System.ArgumentNullException("Bad url", "bad url"); ;
            }
            return itype;
        }

        private void LoadControl()
        {
            switch (CurrentReport)
            {
                case SystemReports.SessionRoll:

                case SystemReports.JudgRoll:
                    //dvCircleByCurrentUser.Visible = true;
                    //dvSessionByCurrentUser.Visible = true;
                    //DivButton.Visible = true;
                    //dvReportParameters.Visible = true;
                   // break;
                case SystemReports.CaseSessionRepo:
                    dvCircleByCurrentUser.Visible = false;
                    dvSessionByCurrentUser.Visible = false;
                    DivButton.Visible = false;
                    dvReportParameters.Visible = false;
                    ReportLoad();
                    break;

                default:
                    break;
            }
        }
        private List<ReportParameter> SetReportPramter()
        {
            // Create the sales order number report parameter  
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(SetNewPramter(ReportParamter.PrintUser.ToString(),PrintUser.ToString()));
            parameters.Add(SetNewPramter(ReportParamter.CourtId.ToString(), CourtID.ToString()));
            switch (CurrentReport)
            {
                case SystemReports.SessionRoll:
                case SystemReports.JudgRoll:

                    //parameters.Add(SetNewPramter(ReportParamter.RollID.ToString(), ddlRolls.SelectedValue));
                    if (RollId > 0)
                    {
                        parameters.Add(SetNewPramter(ReportParamter.RollID.ToString(), RollId.ToString()));
                    }
                    break;

                case SystemReports.CaseSessionRepo:
                    //todo: set param here
                    if (SessionId > 0)
                    {

                        parameters.Add(SetNewPramter(ReportParamter.CaseSessionID.ToString(), SessionId.ToString()));
                    }
                    if (CaseId > 0)
                    {
                        parameters.Add(SetNewPramter(ReportParamter.CaseID.ToString(), CaseId.ToString()));
                    }
                    if (RollId > 0)
                    {
                        parameters.Add(SetNewPramter(ReportParamter.RollID.ToString(), RollId.ToString()));
                    }

                    break;
                default:
                    break;
            }
            return parameters;
        }

        private ReportParameter SetNewPramter(string pramsName, string pramsValue)
        {
            ReportParameter prams = new ReportParameter();
            prams.Name = pramsName;
            prams.Values.Add(pramsValue);

            return prams;
            // return new ReportParameter { Name = pramsName, Values = new StringCollection().Add(pramsValue), Visible = false };
        }
        private void ReportLoad()
        {
            rptViewer.ProcessingMode = ProcessingMode.Remote;

            ServerReport serverReport = rptViewer.ServerReport;

            serverReport.ReportServerUrl = new Uri(SystemConfigurations.Settings_ReportingServiceConfigurations.ReportServerURL);


            serverReport.ReportServerCredentials = new ReportingServiceCredentials();

            serverReport.ReportPath = SystemConfigurations.Settings_ReportingServiceConfigurations.ReportFolderPath + CurrentReport.ToString();
            List<ReportParameter> parameters = SetReportPramter();

            // Set the report parameters for the report  
            rptViewer.ServerReport.SetParameters(parameters);
            dvReport.Visible = true;
            rptViewer.DataBind();
            rptViewer.ServerReport.Refresh();
        }

        protected void ddlCycleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCircuite.DataBind();
        }
    
        protected void ddlCircuite_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRolls.DataBind();
        }
        public IQueryable<vw_KeyValueDate> ddlRolls_GetData()
        {
            int circuit = 0;
            if (ddlCircuite.SelectedIndex>0)
            {
                circuit = int.Parse(ddlCircuite.SelectedValue);
            }
            return sessionServ.SessionByCicuit(circuit);
        }
             public IQueryable<vw_KeyValue> ddlCircuite_GetData()
        {
            
                return circuitService.GetCircuitsByCourtID(this.CourtID).AsQueryable();
            
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            ReportLoad();
        }
    }

    public class ReportingServiceCredentials : IReportServerCredentials
    {
        public bool GetFormsCredentials(out Cookie authCookie,
              out string userName, out string password,
              out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                return new NetworkCredential(SystemConfigurations.Settings_ReportingServiceConfigurations.Username, SystemConfigurations.Settings_ReportingServiceConfigurations.Password, SystemConfigurations.Settings_ReportingServiceConfigurations.DomainName);
            }
        }
    }
}