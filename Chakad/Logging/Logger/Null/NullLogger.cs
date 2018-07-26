using System;

namespace Chakad.Logging.Null
{
    public class NullLogger : ILogger
    {
        private static NullLogger instance = new NullLogger();
        public static NullLogger Instance { get { return instance; } }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogEntry<TState> logEntry)
        {
            return;
        }
    }
}
