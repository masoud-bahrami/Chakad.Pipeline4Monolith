
using Chakad.Logging.Core;
using System;
using System.Collections.Generic;

namespace Chakad.Logging
{
    public class LoggerAggregate
    {
        public List<ILogger> Loggers { get; set; }
        public LogLevel? MinLevel { get; set; }
        public string Category { get; set; }
        public Func<string, LogLevel, bool> Filter;
        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.SystemLog)
                return true;

            if (MinLevel.HasValue)
                return (MinLevel > logLevel);

            if (Filter != null)
                return Filter(Category, logLevel);

            return true;
        }
    }
}
