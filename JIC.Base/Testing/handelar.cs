using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Testing
{
    public class LogeHelper : ILog
    {
        private static readonly Lazy<LogeHelper> instance = new Lazy<LogeHelper>(() => new LogeHelper());

        public static LogeHelper GetInstance
        {
            get
            {
                return instance.Value;
            }
        }
        public void LogException(Exception ex, string url)
        {
            string fileName = string.Format("{0}_{1}.log", "Exception", DateTime.Now.ToShortDateString().Replace('/', '-'));

            string FilePath = @"C:\JicError";

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string logFilePath = string.Format(@"{0}\{1}", FilePath, fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            Guid ExceptionID = Guid.NewGuid();
            sb.AppendLine(string.Concat("Exception ID: ", ExceptionID.ToString()));
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(GetExceptionInfo(ex));

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }

        }
        public static string GetExceptionInfo(Exception ex)
        {

            StringBuilder Entry = new StringBuilder();
            Entry.AppendLine("Exception");
            Entry.AppendLine(string.Concat("Exception Message: ", ex.Message));
            Entry.AppendLine("----------------------------------------------------------------------------");
            Entry.AppendLine("Source: ");
            Entry.AppendLine(ex.Source);
            Entry.AppendLine("----------------------------------------------------------------------------");
            Entry.AppendLine("Stack Trace: ");
            Entry.AppendLine(ex.StackTrace);
            Entry.AppendLine("----------------------------------------------------------------------------");
            Entry.AppendLine(Environment.NewLine);
            Entry.AppendLine(Environment.NewLine);

            if (ex.InnerException != null)
                Entry.Append(GetExceptionInfo(ex.InnerException));
            return Entry.ToString();

        }
    }
    public interface ILog
    {
        void LogException(Exception ex, string url);

    }
}
