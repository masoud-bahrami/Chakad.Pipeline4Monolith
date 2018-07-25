using Chakad.Logging.Core;
using System;
using System.Collections.Concurrent;

namespace Chakad.Logging.File
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, FileLogger> _loggers = new ConcurrentDictionary<string, FileLogger>();
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly FileLoggerSetting _setting;
        public FileLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            _filter = filter;
        }
        public FileLoggerProvider(FileLoggerSetting setting)
        {
            _setting = setting;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation);
        }
        private FileLogger CreateLoggerImplementation(string name)
        {
            return new FileLogger(name, _filter);
        }
        public void Dispose()
        {

        }
    }
}
