using Chakad.Logging.Core;
using System;

namespace Chakad.Logging.File
{
    public class FileLoggerSetting
    {
        public Func<string, LogLevel, bool> Filter;
        public string FileName;
    }
}
