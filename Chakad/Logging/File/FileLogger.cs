using Chakad.Logging.Core;
using System;
using System.Text;
using Chakad.Core;
namespace Chakad.Logging.File
{
    public class FileLogger : ILogger
    {
        #region fields
        private string Name;
        private Func<string, LogLevel, bool> _filter;
        [ThreadStatic]
        private static StringBuilder _logBuilder;
        private readonly string _eventId = "EventId ";
        private readonly string _date = "Date ";
        private string _datePadding;
        private string _messagePadding;
        private string _loglevelPadding = ": ";
        private string _newLineWithMessagePadding;
        #endregion
        #region constructors
        public FileLogger(string name)
            : this(name, "", (n, l) => true)
        {
        }
        public FileLogger(string name, Func<string, LogLevel, bool> filter)
            : this(name, "", filter ?? ((n, l) => true))
        {
        }
        public FileLogger(string name, FileLoggerSetting setting)
            : this(name, setting.FileName, setting.Filter)
        {
        }
        FileLogger(string name, string fileName, Func<string, LogLevel, bool> filter)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = PathHelper.ExecutionPath + @"\chakad_log.log";

            Name = name;
            FileLoggerProcessorConsumerEngeen.FileLog = new FileLog(fileName);
            _filter = filter;
        }
        private void NormilizeFileFormatter(string logLevel)
        {
            var logLevelString = logLevel;

            var length = logLevelString.Length > _eventId.Length ? logLevelString.Length : _eventId.Length;

            _messagePadding = new string(' ', length + _loglevelPadding.Length);
            _newLineWithMessagePadding = Environment.NewLine + _messagePadding;
            _datePadding = new string(' ', length - _date.Length );
        }
        #endregion

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Check if this FileLogger accept messages at specified log level
        /// </summary>
        /// <param name="logLevel">Specified log level to check if FileLogger is enable for this level</param>
        /// <returns>If loglevel not set to None, and also FileLogger.Loglevel is lesser than loglevel return true</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.SystemLog)
                return true;

            if (logLevel == LogLevel.None)
                return false;

            return _filter(Name, logLevel);
        }

        public void Log<TState>(LogEntry<TState> logEntry)
        {
            if (!IsEnabled(logEntry.LogLevel))
                return;

            if (logEntry.Formatter == null)
            {
                throw new ArgumentNullException(nameof(logEntry.Formatter));
            }

            if (logEntry.Exception != null)
            {
                WriteMessage(logEntry);
            }
        }

        private void WriteMessage<TState>(LogEntry<TState> logEntry)
        {
            var logLevelString = string.Empty;
            logLevelString = logEntry.ParseLogLevel();
            NormilizeFileFormatter(logLevelString);

            var logBuilder = _logBuilder;
            _logBuilder = null;

            if (logBuilder == null)
            {
                logBuilder = new StringBuilder();
            }

            // Example:
            // INFO: ConsoleApp.Program[10]
            //       Request received
            
            // category and event id
            logBuilder.Append(_eventId);
            logBuilder.Append(_loglevelPadding);
            logBuilder.Append("[");
            logBuilder.Append(logEntry.Id);
            logBuilder.AppendLine("]");

            logBuilder.Append(_date);
            logBuilder.Append(_datePadding);
            logBuilder.Append(_loglevelPadding);
            logBuilder.Append("[");
            logBuilder.Append(logEntry.Date);
            logBuilder.AppendLine("]");

            if (logEntry.LogObject != null)
            {
                // message
                logBuilder.Append(_messagePadding);

                var serialiedObject = Newtonsoft.Json.JsonConvert.SerializeObject(logEntry.LogObject);

                var len = logBuilder.Length;
                logBuilder.AppendLine(serialiedObject);
                logBuilder.Replace(Environment.NewLine, _newLineWithMessagePadding, len, serialiedObject.Length);
            }

            var message = logEntry.Formatter(logEntry.State, logEntry.Exception);

            if (!string.IsNullOrEmpty(message))
            {
                // message
                logBuilder.Append(_messagePadding);

                var len = logBuilder.Length;
                logBuilder.AppendLine(message);
                logBuilder.Replace(Environment.NewLine, _newLineWithMessagePadding, len, message.Length);
            }

            // Example:
            // System.InvalidOperationException
            //    at Namespace.Class.Function() in File:line X
            if (logEntry.Exception != null)
            {
                // exception message
                logBuilder.AppendLine(logEntry.Exception.ToString());
            }

            if (logBuilder.Length > 0)
            {
                var hasLevel = !string.IsNullOrEmpty(logLevelString);
                // Queue log message
                FileLoggerProcessorConsumerEngeen.EnqueueMessage(new LogMessageEntry
                {
                    Message = logBuilder.ToString(),
                    LevelString = hasLevel ? logLevelString : null
                });
            }

            logBuilder.Clear();
            if (logBuilder.Capacity > 1024)
            {
                logBuilder.Capacity = 1024;
            }
            _logBuilder = logBuilder;
        }
    }
}