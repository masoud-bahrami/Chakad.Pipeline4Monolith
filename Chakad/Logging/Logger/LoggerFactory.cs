using Chakad.Logging;
using System;
using System.Collections.Generic;

namespace Chakad.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        private List<ILoggerProvider> providers = new List<ILoggerProvider>();
        private static Dictionary<string, ILogger> _loggers = new Dictionary<string, ILogger>();

        public ILogger CreateLogger(string categoryName)
        {
            if (!_loggers.TryGetValue(categoryName, out ILogger logger))
            {
                var loggerAggregate = new LoggerAggregate
                {
                    Category = categoryName,
                    MinLevel = GetMinLevel(),
                    Filter = GetFilter(),
                    Loggers = GetLoggers(categoryName)
                };

                logger = new Logger(loggerAggregate);
            }
            return logger;
        }

        private Func<string, LogLevel, bool> GetFilter()
        {
            return null;
        }

        private LogLevel? GetMinLevel()
        {
            return null;
        }

        private List<ILogger> GetLoggers(string category)
        {
            List<ILogger> loggers = new List<ILogger>();
            providers.ForEach(provider => loggers.Add(provider.CreateLogger(category)));
            return loggers;
        }

        public void AddProvider(ILoggerProvider provider)
        {
            if (!providers.Contains(provider))
            {
                providers.Add(provider);
            }
        }

        public void Dispose()
        {
        }
    }
}