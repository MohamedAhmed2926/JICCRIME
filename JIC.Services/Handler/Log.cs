using JIC.Base;
using JIC.Base.Interfaces;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.Handler
{
    public class Log : Base.Interfaces.ILogger
    {
        private static Base.Interfaces.ILogger logger;
        private Logger loggerConfiguration;
        public Log()
        {
            loggerConfiguration = new Serilog.LoggerConfiguration()
            .WriteTo.RollingFile(SystemConfigurations.LogPath)
            .CreateLogger();
        }

        public static Base.Interfaces.ILogger GetLogger()
        {
            if (logger == null)
                logger = new Log();

            return logger;
        }

        public void LogError(string message)
        {
            loggerConfiguration.Error(message);
        }

        public void LogException(Exception exception)
        {
            // To See Entity Errors
            if (exception is DbEntityValidationException)
            {
                var DbEntityValidation = (DbEntityValidationException)exception;
            }
            

            loggerConfiguration.Error(exception,exception.Message);
        }

        public void LogInformation(string message)
        {
            loggerConfiguration.Information(message);
        }
        
    }
}
