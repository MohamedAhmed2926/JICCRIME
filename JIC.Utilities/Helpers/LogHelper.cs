using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using JIC.Base;
namespace JIC.Utilities.Helpers
{
    public class LogHelper
    {
        /// <summary>
        /// Adding the exception data to the windows event log.
        /// </summary>
        /// <param name="ex">The thrown exception.</param>
        /// <param name="Source">The source of the exception.</param>        
        public static Guid LogException(Exception ex, string Source)
        {
            try
            {
                if (!(ex is ThreadAbortException))
                {
                    StringBuilder LogEntry = new StringBuilder();
                    Guid ExceptionID = Guid.NewGuid();
                    LogEntry.AppendLine(string.Concat("Exception ID: ", ExceptionID.ToString()));
                    LogEntry.AppendLine(string.Concat("Exception Source: ", Source));
                    LogEntry.Append(GetExceptionInfo(ex));
                    AddLogEntry(SystemConfigurations.Settings_LogSourceName, LogEntry.ToString(), EventLogEntryType.Error);
                    return ExceptionID;
                }
            }
            catch
            {

            }
            return Guid.Empty;
        }

        /// <summary>
        /// Writes a warning message to the windows event log.
        /// </summary>
        /// <param name="warning"></param>
        public static void LogWarning(string Warning)
        {
            try
            {
                AddLogEntry(SystemConfigurations.Settings_LogSourceName, Warning, EventLogEntryType.Warning);
            }
            catch
            {

            }

        }

        /// <summary>
        /// Writes a specific message to the windows event log.
        /// </summary>
        /// <param name="Message">The thrown exception.</param>        
        public static void LogMessage(string Message)
        {
            try
            {
                AddLogEntry(SystemConfigurations.Settings_LogSourceName, Message, EventLogEntryType.Information);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Returns the string represents the exception info in addition to the inner exceptions info.
        /// </summary>
        /// <param name="ex">The excepion you need to return its' info.</param>
        /// <returns></returns>
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

        #region Private Methods

        /// <summary>
        /// Adds an entry to the windows event log.
        /// </summary>
        /// <param name="LogSource">The source by which the application is registered on the specified computer.</param>
        /// <param name="Message">The string to write to the event log.</param>
        /// <param name="EntryType">One of the System.Diagnostics.EventLogEntryType values.</param>
        static void AddLogEntry(string LogSource, string Message, EventLogEntryType EntryType)
        {
            EventLog.WriteEntry(LogSource, Message, EntryType);
        }

        #endregion

    }
}
