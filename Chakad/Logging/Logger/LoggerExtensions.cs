using Chakad.Logging.Core;
using Chakad.Logging.Core.Internal;
using System;
using System.Collections.Generic;

namespace Chakad.Logging
{
    /// <summary>
    /// ILogger extension methods for common scenarios. By Microsoft
    /// </summary>
    public static class LoggerExtensions
    {
        private static readonly Func<object, Exception, string> _messageFormatter = MessageFormatter;

        //------------------------------------------DEBUG------------------------------------------//

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogDebug(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            var logEntry = new LogEntry<string>
            {
                EventId = eventId,
                Exception = exception,
                Formatter = _messageFormatter,
                LogLevel = LogLevel.Debug,
                State = message
            };
            logger.DoLog(LogLevel.Debug, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogDebug(this ILogger logger, EventId eventId, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogDebug(this ILogger logger, EventId eventId, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug(0, "Processing request from {Address}", address)</example>
        public static void LogDebug(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, eventId, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogDebug(this ILogger logger, EventId eventId, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, eventId, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogDebug(this ILogger logger, EventId eventId, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, eventId, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }
        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug(exception, "Error while processing request from {Address}", address)</example>
        public static void LogDebug(this ILogger logger, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, 0, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogDebug(this ILogger logger, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, 0, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogDebug(this ILogger logger, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, 0, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug(0, "Processing request from {Address}", address)</example>
        public static void LogDebug(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, 0, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogDebug(this ILogger logger, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, 0, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogDebug(this ILogger logger, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Debug, 0, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }
        //------------------------------------------TRACE------------------------------------------//

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogTrace(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogTrace(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogTrace(this ILogger logger, EventId eventId, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogTrace(this ILogger logger, EventId eventId, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogTrace(0, "Processing request from {Address}", address)</example>
        public static void LogTrace(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, eventId, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogTrace(this ILogger logger, EventId eventId, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, eventId, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogTrace(this ILogger logger, EventId eventId, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, eventId, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }
        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogTrace(exception, "Error while processing request from {Address}", address)</example>
        public static void LogTrace(this ILogger logger, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, 0, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogTrace(this ILogger logger, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, 0, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogTrace(this ILogger logger, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, 0, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogTrace("Processing request from {Address}", address)</example>
        public static void LogTrace(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, 0, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogTrace(this ILogger logger, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, 0, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogTrace(this ILogger logger, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Trace, 0, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        //------------------------------------------INFORMATION------------------------------------------//

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogInformation(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogInformation(this ILogger logger, EventId eventId, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogInformation(this ILogger logger, EventId eventId, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(0, "Processing request from {Address}", address)</example>
        public static void LogInformation(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, eventId, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogInformation(this ILogger logger, EventId eventId, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, eventId, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogInformation(this ILogger logger, EventId eventId, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, eventId, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(exception, "Error while processing request from {Address}", address)</example>
        public static void LogInformation(this ILogger logger, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, 0, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogInformation(this ILogger logger, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, 0, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogInformation(this ILogger logger, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, 0, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation("Processing request from {Address}", address)</example>
        public static void LogInformation(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            //logger.Log(LogLevel.Information, 0, new FormattedLogValues(message, args), null, _messageFormatter);
            logger.DoLog(LogLevel.Information, 0, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogInformation(this ILogger logger, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, 0, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogInformation(this ILogger logger, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Information, 0, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }


        //------------------------------------------SystemLog------------------------------------------//

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogSystemLog(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogSystemLog(this ILogger logger, EventId eventId, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogSystemLog(this ILogger logger, EventId eventId, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(0, "Processing request from {Address}", address)</example>
        public static void LogSystemLog(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, eventId, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogSystemLog(this ILogger logger, EventId eventId, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, eventId, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogSystemLog(this ILogger logger, EventId eventId, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, eventId, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(exception, "Error while processing request from {Address}", address)</example>
        public static void LogSystemLog(this ILogger logger, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, 0, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogSystemLog(this ILogger logger, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, 0, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogSystemLog(this ILogger logger, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, 0, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation("Processing request from {Address}", address)</example>
        public static void LogSystemLog(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, 0, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogSystemLog(this ILogger logger, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, 0, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogSystemLog(this ILogger logger, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.SystemLog, 0, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        //------------------------------------------WARNING------------------------------------------//

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogWarning(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogWarning(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogWarning(this ILogger logger, EventId eventId, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogWarning(this ILogger logger, EventId eventId, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogWarning(0, "Processing request from {Address}", address)</example>
        public static void LogWarning(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, eventId, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogWarning(this ILogger logger, EventId eventId, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, eventId, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogWarning(this ILogger logger, EventId eventId, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, eventId, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogWarning(exception, "Error while processing request from {Address}", address)</example>
        public static void LogWarning(this ILogger logger, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, 0, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogWarning(this ILogger logger, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, 0, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogWarning(this ILogger logger, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, 0, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogWarning("Processing request from {Address}", address)</example>
        public static void LogWarning(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, 0, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogWarning(this ILogger logger, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, 0, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogWarning(this ILogger logger, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Warning, 0, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        //------------------------------------------ERROR------------------------------------------//

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogError(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogError(this ILogger logger, EventId eventId, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogError(this ILogger logger, EventId eventId, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError(0, "Processing request from {Address}", address)</example>
        public static void LogError(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, eventId, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogError(this ILogger logger, EventId eventId, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, eventId, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogError(this ILogger logger, EventId eventId, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, eventId, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError(exception, "Error while processing request from {Address}", address)</example>
        public static void LogError(this ILogger logger, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, 0, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogError(this ILogger logger, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, 0, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogError(this ILogger logger, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, 0, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError("Processing request from {Address}", address)</example>
        public static void LogError(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, 0, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogError(this ILogger logger, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, 0, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogError(this ILogger logger, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Error, 0, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        //------------------------------------------CRITICAL------------------------------------------//

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, eventId, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, "Processing request from {Address}", address)</example>
        public static void LogCritical(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, eventId, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogCritical(this ILogger logger, EventId eventId, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, eventId, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogCritical(this ILogger logger, EventId eventId, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, eventId, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogCritical(this ILogger logger, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, 0, new FormattedLogValues(message, args), exception, _messageFormatter);
        }
        public static void LogCritical(this ILogger logger, Exception exception, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, 0, new FormattedLogValues(message, args), exception, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogCritical(this ILogger logger, Exception exception, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, 0, new FormattedLogValues(message, args), exception, _messageFormatter, id);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical("Processing request from {Address}", address)</example>
        public static void LogCritical(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, 0, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void LogCritical(this ILogger logger, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, 0, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void LogCritical(this ILogger logger, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(LogLevel.Critical, 0, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        public static void Log<TState>(this ILogger logger, LogEntry<TState> logEntry)
        {
            if (logEntry == null)
            {
                throw new ArgumentNullException(nameof(logEntry));
            }

            logger.Log(logEntry);
        }
        /// <summary>
        /// Formats and writes a log message on specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Log(this ILogger logger, LogLevel logLevel, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(logLevel, 0, new FormattedLogValues(message, args), null, _messageFormatter);
        }
        public static void Log(this ILogger logger, LogLevel logLevel, string logObjectName, object logObject, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(logLevel, 0, new FormattedLogValues(message, args), null, _messageFormatter, "", logObjectName, logObject);
        }
        public static void Log(this ILogger logger, LogLevel logLevel, string id, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.DoLog(logLevel, 0, new FormattedLogValues(message, args), null, _messageFormatter, id);
        }

        //------------------------------------------Scope------------------------------------------//

        /// <summary>
        /// Formats the message and creates a scope.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to create the scope in.</param>
        /// <param name="messageFormat">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A disposable scope object. Can be null.</returns>
        /// <example>
        /// using(logger.BeginScope("Processing request from {Address}", address))
        /// {
        /// }
        /// </example>
        public static IDisposable BeginScope(
            this ILogger logger,
            string messageFormat,
            params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            return logger.BeginScope(new FormattedLogValues(messageFormat, args));
        }

        //------------------------------------------HELPERS------------------------------------------//

        private static string MessageFormatter(object state, Exception error)
        {
            return state.ToString();
        }
        private static void DoLog<TState>(this ILogger logger,
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter,
            string id = "",
            string logObjectName = "",
            object logObject = null,
            string source = "",
            ICollection<string> tags = null)
        {
            var logEntry = new LogEntry<TState>
            {
                EventId = eventId,
                State = state,
                Exception = exception,
                Formatter = formatter,
                LogObjectName = logObjectName,
                LogObject = logObject,
                Source = source,
                Id = id,
                Tags = tags
            };

            logger.Log(logEntry);
        }
    }
}