using System;

namespace Library.Shared.Logging
{
    public interface ILogger
    {
        void Trace(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Error(string message, Exception exception);
    }
}