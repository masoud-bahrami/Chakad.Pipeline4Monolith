using Chakad.Core;
using System;
using System.Collections.Generic;

namespace Chakad.Logging
{
    public class LogEntry<TState>
    {
        public LogEntry()
        {
            Date = DateTime.Now;
            ServiceId = ServiceSpecification.ServiceId;
            InstanceId = ServiceSpecification.InstanceId;
        }
        /// <summary>
        /// The source of the log
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// The Service/App generate this log
        /// </summary>
        public string ServiceId { get; }
        public string InstanceId { get;  }
        /// <summary>
        /// Tags related to this event log.
        /// </summary>
        public ICollection<string> Tags { get; set; }
        /// <summary>
        /// Entry will be written on this level
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// Id of the event
        /// </summary>
        public EventId EventId { get; set; }
        /// <summary>
        /// The entry to be written. Can be also an object.
        /// </summary>
        public TState State { get; set; }
        /// <summary>
        /// The exception related to this entry.
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// Function to create a <c>string</c> message of the <paramref name="state"/> and <paramref name="exception"/>.
        /// </summary>
        public Func<TState, Exception, string> Formatter { get; set; }
        public string LogObjectName { get; set; }
        public object LogObject { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// The Date this log occured
        /// </summary>
        public DateTime Date { get;  }
        public string ParseLogLevel()
        {
            switch (LogLevel)
            {
                case LogLevel.Trace:
                    return "Trace";
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Information:
                    return "Info";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.Error:
                    return "Fail";
                case LogLevel.Critical:
                    return "Critical";
                case LogLevel.SystemLog:
                    return "SysLog";
                default:
                    throw new ArgumentOutOfRangeException(nameof(LogLevel));
            }
        }
    }
}
