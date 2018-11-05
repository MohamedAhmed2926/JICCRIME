using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Collections.Specialized;

namespace JIC.Base
{

    public class SystemConfigurations
    {

        public class SecurityServiceConfigurations
        {
            public string UserId { get; set; }
            public string Username { get; set; }
            public string UserTypeId { get; set; }
            public string Password { get; set; }
            public string CourtID { get; set; }
            public string CourtName { get; set; }
        }
        #region DateTime

        /// <summary>
        /// returns the format of the long date.
        /// </summary>
        public static string DateTime_LongDateFormat { get { return ConfigurationManager.AppSettings.Get("DateTime_LongDateFormat") ?? "dddd dd/MM/yyyy"; } }
        /// <summary>
        /// returns the format of the short date.
        /// </summary>
        public static string DateTime_ShortDateFormat { get { return ConfigurationManager.AppSettings.Get("DateTime_ShortDateFormat") ?? "dd/MM/yyyy"; } }
        /// <summary>
        /// returns the format of the date and time.
        /// </summary>
        public static string DateTime_DateTimeFormat { get { return ConfigurationManager.AppSettings.Get("DateTime_DateTimeFormat") ?? "dd/MM/yyyy tt hh:mm:ss"; } }
        /// <summary>
        /// returns the format of the time.
        /// </summary>
        public static string DateTime_TimeFormat { get { return ConfigurationManager.AppSettings.Get("DateTime_TimeFormat") ?? "tt hh:mm"; } }

        #endregion

        #region GridView

        /// <summary>
        /// returns the size of the small grid view page.
        /// </summary>
        public static int GridView_SmallPageSize { get { return int.Parse(ConfigurationManager.AppSettings.Get("GridView_SmallPageSize") ?? "10"); } }
        /// <summary>
        /// returns the size of the medium grid view page.
        /// </summary>
        public static int GridView_MediumPageSize { get { return int.Parse(ConfigurationManager.AppSettings.Get("GridView_MediumPageSize") ?? "25"); } }
        /// <summary>
        /// returns the size of the large grid view page.
        /// </summary>
        public static int GridView_LargePageSize { get { return int.Parse(ConfigurationManager.AppSettings.Get("GridView_LargePageSize") ?? "50"); } }

        #endregion

        #region Settings
        /// <summary>
        /// returns the name of the event log source.
        /// </summary>
        public static string Settings_LogSourceName { get { return ConfigurationManager.AppSettings.Get("Settings_LogSourceName"); } }

        /// <summary>
        /// Indecates if the system should work in development mode or live mode.
        /// </summary>
        public static bool Settings_InDevelopmentMode { get { return bool.Parse(ConfigurationManager.AppSettings.Get("Settings_InDevelopmentMode")??"true"); } }

        /// <summary>
        /// Returns the path of the external connection string file.
        /// </summary>
        private static string Settings_ConnectionStringFilePath { get { return ConfigurationManager.AppSettings.Get("Settings_ConnectionStringFilePath") ?? @"c:\jic\connection_string.txt"; } }

        /// <summary>
        /// Returns The End Month of The Working Year.
        /// </summary>
        public static int Settings_CircuitEndMonth
        {
            get { return int.Parse(ConfigurationManager.AppSettings.Get("Settings_CircuitEndMonth") ?? "9"); }
        }

        /// <summary>
        /// Returns The End Day of The Working Year.
        /// </summary>
        public static int Settings_CircuitEndDay
        {
            get { return int.Parse(ConfigurationManager.AppSettings.Get("Settings_CircuitEndDay") ?? "30"); }
        }

        /// <summary>
        /// Returns The End Month of The Working Year.
        /// </summary>
        public static int Settings_CircuitEndMonthInYear
        {
            get { return int.Parse(ConfigurationManager.AppSettings.Get("Settings_CircuitEndMonthInYear") ?? "12"); }
        }

        /// <summary>
        /// Returns The End Day of The Working Year.
        /// </summary>
        public static int Settings_CircuitEndDayInYear
        {
            get { return int.Parse(ConfigurationManager.AppSettings.Get("Settings_CircuitEndDayInYear") ?? "31"); }
        }

        /// <summary>
        /// Returns the connection string to be used to build the data context.
        /// </summary>
        public static string Settings_ConnectionString
        {
            get
            {
                if (!Settings_InDevelopmentMode)
                    return ConfigurationManager.ConnectionStrings["JICContext"].ConnectionString;
                else
                    return System.IO.File.ReadAllLines(Settings_ConnectionStringFilePath)[0];
            }
        }

        /// <summary>
        /// Read From Cinfig File the Services URLs
        /// Split using ; for court with multiple services
        /// Primary Court with multiple initial courts and send an 'إستئناف'
        /// So i don't know which court has the data
        /// </summary>
        public static IDictionary<int,List<string>> FaultCourtWebServices
        {
            get
            {
                IDictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
                NameValueCollection FaultCourtServiceSection = (NameValueCollection)ConfigurationManager.GetSection("FaultCourtServices");
                foreach (var CourtID in FaultCourtServiceSection.AllKeys.Select(Key=>int.Parse(Key)))
                {
                    dictionary[CourtID] = FaultCourtServiceSection[CourtID.ToString()].Split(';').Distinct().ToList();
                }
                return dictionary;
            }
        }

        /// <summary>
        /// Returns the path of the external reporting services file.
        /// </summary>
        public static string Settings_ReportingServicesFilePath { get { return ConfigurationManager.AppSettings.Get("Settings_ReportingServicesFilePath") ?? @"c:\jic\reporting_services.txt"; } }

        /// <summary>
        /// Returns the configurations to be used to connect to reporting services and generate reports.
        /// </summary>
        public static ReportingServiceConfigurations Settings_ReportingServiceConfigurations
        {
            get
            {
                if (!Settings_InDevelopmentMode)
                    return new ReportingServiceConfigurations
                    {
                        ReportServerURL = ConfigurationManager.AppSettings.Get("ReportingServices_ReportServerURL"),
                        ReportFolderPath = ConfigurationManager.AppSettings.Get("ReportingServices_ReportFolderPath"),
                        DomainName = ConfigurationManager.AppSettings.Get("ReportingServices_DomainName"),
                        Username = ConfigurationManager.AppSettings.Get("ReportingServices_Username"),
                        Password = ConfigurationManager.AppSettings.Get("ReportingServices_Password"),
                    };
                else
                {
                    string[] lines = System.IO.File.ReadAllLines(Settings_ReportingServicesFilePath);
                    return new ReportingServiceConfigurations
                    {
                        ReportServerURL = lines[0],
                        ReportFolderPath = lines[1],
                        DomainName = lines[2],
                        Username = lines[3],
                        Password = lines[4],
                    };
                }
            }
        }

        /// <summary>
        /// Returns the temp files folder path.
        /// </summary>
        public static string Settings_TempFilesFolderPath { get { return ConfigurationManager.AppSettings.Get("Settings_TempFilesFolderPath") ?? @"~/Files/Temp/"; } }

        #endregion

        #region SMTP

        public static string GetSMTPProperty(EmailFunctions Function, SMTPProperties Property)
        {
            return ConfigurationManager.AppSettings.Get(string.Format("SMTP_{0}.{1}", Function.ToString(), Property.ToString()));
        }

        #endregion

        #region Defaults

        /// <summary>
        /// returns the default allowed max files upload count.
        /// </summary>
        public static int Defaults_MaxFilesUploadCount { get { return int.Parse(ConfigurationManager.AppSettings.Get("Defaults_MaxFilesUploadCount") ?? "5"); } }

        /// <summary>
        /// returns the default max files size in mb.
        /// </summary>
        public static long Defaults_MaxFileSize { get { return long.Parse(ConfigurationManager.AppSettings.Get("Defaults_MaxFileSize") ?? "2"); } }
        public static string Defaults_DefaultPassword { get { return ConfigurationManager.AppSettings.Get("Defaults_DefaultPassword") ?? "1234"; } }
        public static long Defaults_CurrentCourtType { get { return long.Parse(ConfigurationManager.AppSettings.Get("Defaults_CurrentCourtType") ?? "01"); } }
        public static int EgyptNationalityID { get { return 1; } }
        public static int CompaniesClassificationID { get { return 6; } }

        /// <summary>
        /// return the Default Log Path
        /// </summary>
        public static string LogPath { get { return ConfigurationManager.AppSettings.Get("LogPath") ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log-{Date}.txt"); } }
        public static string FailedProsResponse { get { return ConfigurationManager.AppSettings.Get("FaildProsString") ?? "failed"; } }
        public static string SuccessProsResponse { get { return ConfigurationManager.AppSettings.Get("SuccessProsString") ?? "success"; } }
        public static int FailedProsResponseInt { get { return int.Parse(ConfigurationManager.AppSettings.Get("FailedProsInt") ?? "0"); } }
        
        public static int ScheudlerInterval
        {
            get
            {
                int interval;
                if (int.TryParse(ConfigurationManager.AppSettings.Get("schedulerInterval"), out interval))
                    return interval;

                return 15;
            }
        }
        #endregion

        public class ReportingServiceConfigurations
        {
            public string ReportServerURL { get; set; }
            public string ReportFolderPath { get; set; }
            public string DomainName { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
