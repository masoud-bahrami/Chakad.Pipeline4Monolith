using Chakad.Logging;
using System;
using System.Collections.Generic;

namespace Chakad.Logging
{
    public class Logger : ILogger
    {
        public LoggerAggregate _loggerAggregate;
        public Logger(LoggerAggregate loggerAggregate)
        {
            _loggerAggregate = loggerAggregate;
        }

        public void Log<TState>(LogEntry<TState> logEntry)
        {
            foreach (var logger in _loggerAggregate.Loggers)
            {
                if (!IsEnabled(logEntry.LogLevel))
                    return;

                if (!logger.IsEnabled(logEntry.LogLevel))
                    return;

                List<Exception> exceptions = null;
                try
                {
                    logger.Log(logEntry);
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                        exceptions = new List<Exception>();

                    exceptions.Add(ex);
                }

                if (exceptions != null && exceptions.Count > 0)
                {
                    throw new AggregateException(
                        message: "An error occurred while writing to logger(s).", innerExceptions: exceptions);
                }
            }
        }

        /// <summary>
        /// Checks if the given <paramref name="logLevel"/> is enabled.
        /// </summary>
        /// <param name="logLevel">level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return _loggerAggregate.IsEnabled(logLevel);
        }

        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}