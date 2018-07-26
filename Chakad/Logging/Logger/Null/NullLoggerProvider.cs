using System;

namespace Chakad.Logging.Null
{
    public class NullLoggerProvider: ILoggerProvider
    {
        private static NullLoggerProvider instance = new NullLoggerProvider();
        public static NullLoggerProvider Instance { get { return instance; } }
        public ILogger CreateLogger(string categoryName)
        {
            return  NullLogger.Instance;
        }

        public void Dispose()
        {
        }
    }
}
