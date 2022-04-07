using System;
using NLog;

namespace Library.Shared.Logging
{
    public class Logger : ILogger
    {
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public void Trace(string message) => logger.Trace(message);

        public void Info(string message) => logger.Info(message);

        public void Warning(string message) => logger.Warn(message);

        public void Error(string message) => logger.Error(message);

        public void Error(string message, Exception exception) => logger.Error(exception, message);
    }
}