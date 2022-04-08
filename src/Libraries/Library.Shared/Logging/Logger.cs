using System;
using NLog;

namespace Library.Shared.Logging
{
    public class Logger : ILogger
    {
        private static readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        public void Trace(string message) => _logger.Trace(message);

        public void Info(string message) => _logger.Info(message);

        public void Warning(string message) => _logger.Warn(message);

        public void Error(string message) => _logger.Error(message);

        public void Error(string message, Exception exception) => _logger.Error(exception, message);
    }
}