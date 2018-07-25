using System;
using System.Collections.Generic;
using System.Linq;
using Chakad.Container;
using Chakad.Core;
using Chakad.Logging;
using Chakad.Logging.Core;
using Chakad.Pipeline.Core.Internal;

namespace Chakad.Pipeline
{
    public static class ConfigureExtensions
    {
        private readonly static string loggerCategoryName = "Chakad.Configure";
        private static ILogger Logger
        {
            get
            {
                return LoggerBuilder.Instance.LoggerFactory.CreateLogger(loggerCategoryName);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handles"></param>
        /// <returns></returns>
        public static Configure Order(this Configure configure, Type type, List<Type> handles)
        //where T : IDomainEvent 
        //where THandler : IWantToHandleEvent<T>
        {
            Logger.LogSystemLog(EventIdConstants.ChakadSetOrder,
              $"Set orders of receiving events by subscribers for event ={type.FullName}");

            string subOrderName = "";
            handles.ForEach(h => subOrderName += $"'{h.FullName}' ");

            Logger.LogSystemLog(EventIdConstants.ChakadSetOrder,
              $"Subscribers order for event={type.FullName} are {handles}");

            OrderConfiger.SetOrder(type, handles);
            return configure;
        }
        public static Configure SetServiceSpecifications(this Configure configure, string serviceId, string instanceId)
        {
            ServiceSpecification.ServiceId = serviceId;
            ServiceSpecification.InstanceId = instanceId;
            return configure;
        }

        #region logger
        public static LoggerBuilder SetLoggerFactory(this Configure configure, ILoggerFactory factory)
        {
            LoggerBuilder.Instance.SetFactory(factory);
            return LoggerBuilder.Instance;
        }
        public static LoggerBuilder SetDefaultLoggerFactory(this Configure configure)
        {
            var factory = new LoggerFactory();

            LoggerBuilder.Instance.SetFactory(factory);
            return LoggerBuilder.Instance;
        }

        public static Configure SetLoggerProviders(this LoggerBuilder loggerExtensions, params ILoggerProvider[] providers)
        {
            foreach (var provider in providers)
                loggerExtensions.LoggerFactory.AddProvider(provider);

            var providerNames = "";
            providers.ToList().ForEach(p => providerNames += $"'{p.GetType().FullName}' ");

            Logger.LogSystemLog(EventIdConstants.ChakadSetLoggerProvider,
            $"Chakad. Set Logger Providers. {providerNames}");

            Logger.LogSystemLog(EventIdConstants.ChakadLoggerFactory,
             $"Chakad. Logger Factory. {loggerExtensions.LoggerFactory.GetType().FullName}");

            return Configure.Instance;
        }

        public static Configure SetDefaultLoggerProviders(this LoggerBuilder loggerExtensions, string logFileName, Func<string, LogLevel, bool> filter)
        {
            var provider = FileLoggerFactory.GetFileLogProvider(logFileName, filter);

            loggerExtensions.LoggerFactory.AddProvider(provider);

            Logger.LogSystemLog(EventIdConstants.ChakadSetDefaultLoggerProvider,
                $"Chakad. Set Default Logger Provider with logFileName={logFileName} and filter={filter}");

            Logger.LogSystemLog(EventIdConstants.ChakadSetDefaultLoggerProvider,
                $"Chakad. Default logger provider is {provider.GetType().FullName}");

            Logger.LogSystemLog(EventIdConstants.ChakadLoggerFactory,
             $"Chakad. Logger Factory. {loggerExtensions.LoggerFactory.GetType().FullName}");

            return Configure.Instance;
        }
        public static Configure SetDefaultLoggerProviders(this LoggerBuilder loggerExtensions, Func<string, LogLevel, bool> filter)
        {
            var provider = FileLoggerFactory.GetFileLogProvider(filter);

            loggerExtensions.LoggerFactory.AddProvider(provider);

            Logger.LogSystemLog(EventIdConstants.ChakadSetDefaultLoggerProvider,
                $"Chakad. Set Default Logger Provider with logFileName=null and filter=null");

            Logger.LogSystemLog(EventIdConstants.ChakadSetDefaultLoggerProvider,
               $"Chakad. Default logger provider is {provider.GetType().FullName}");

            Logger.LogSystemLog(EventIdConstants.ChakadLoggerFactory,
            $"Chakad. Logger Factory. {loggerExtensions.LoggerFactory.GetType().FullName}");
            return Configure.Instance;
        }
        public static Configure SetDefaultLoggerProviders(this LoggerBuilder loggerExtensions)
        {
            var provider = FileLoggerFactory.GetFileLogProvider();
            loggerExtensions.LoggerFactory.AddProvider(provider);

            Logger.LogSystemLog(EventIdConstants.ChakadSetDefaultLoggerProvider,
               $"Chakad. Set Default Logger Provider with logFileName=null and filter = null");

            Logger.LogSystemLog(EventIdConstants.ChakadSetDefaultLoggerProvider,
            $"Chakad. Default logger provider is {provider.GetType().FullName}");

            Logger.LogSystemLog(EventIdConstants.ChakadLoggerFactory,
                $"Chakad. Logger Factory. {loggerExtensions.LoggerFactory.GetType().FullName}");

            return Configure.Instance;
        }
        #endregion
        #region Container
        public static Configure SetContainer(this Configure configure, IChakadContainer container)
        {
            Logger.LogSystemLog(EventIdConstants.ChakadSetContainer,
                $"Chakad. Set Custom Container. {container.GetType().FullName}");

            ChakadContainer.Run(container);
            return configure;
        }
        public static Configure SetDefaultContainer(this Configure configure)
        {
            Logger.LogInformation(EventIdConstants.ChakadSetDefaultContainer,
                $"Chakad. Set Default Container. {typeof(DefaultContainer).FullName}");

            ChakadContainer.Run(new DefaultContainer());
            return configure;
        }
        #endregion
    }
}