using System;
using System.IO;
using System.Reflection;

namespace Chakad.Logging.File
{
    public static class FileLoggerFactoryExtensions
    {
        public static ILoggerFactory AddFileLogger(this ILoggerFactory factory)
        {
            var basePath = Assembly.GetEntryAssembly().Location;

            var fileAddress = Path.Combine(basePath, "chakad.file.logger");

            return AddFileLogger(factory, (n, l) => l >= LogLevel.Information, fileAddress);
        }

        public static ILoggerFactory AddFileLogger(this ILoggerFactory factory, string fileAddress)
        {
            return AddFileLogger(factory, (n, l) => l >= LogLevel.Information, fileAddress);
        }

        public static ILoggerFactory AddFileLogger(this ILoggerFactory factory, Func<string, LogLevel, bool> filter,
            string fileAddress)
        {
            //factory.AddProvider(new FileLoggerProvider(filter, fileAddress));
            return factory;
        }
    }
}