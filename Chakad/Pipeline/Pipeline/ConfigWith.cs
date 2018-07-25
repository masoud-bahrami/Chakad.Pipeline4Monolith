using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chakad.Container;
using Chakad.Core;
using Chakad.Logging.Core;
using Chakad.Pipeline.Core.Event;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Pipeline.Core.Internal;
using Chakad.Logging;

namespace Chakad.Pipeline
{
    public static class ConfigWith
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
        /// <param name="assemblyPath"></param>
        /// <param name="assemblyNameContain"></param>
        /// <param name="filterByDll"></param>
        public static Configure With(this Configure configure, string assemblyPath, string assemblyNameContain = "", bool filterByDll = true)
        {
            Logger.LogSystemLog(EventIdConstants.InitialiingChakad,
               $"Start initializing Chakad with assemblyPath ={assemblyPath} , assemblyNameContain={assemblyNameContain} , filterByDll={filterByDll}.");

            var types = TypeHelper.GetTypes(assemblyPath, assemblyNameContain, filterByDll, typeof(IWantToSubscribeThisEventInterface)
                , typeof(IHandleMessage));
            With(configure,types);
            return configure;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        public static Configure With(this Configure configure, List<Type> types)
        {
            Logger.LogSystemLog(EventIdConstants.ChakadInitilizingTypes,
               $"Chakad Initializing types count is ={types.Count} ");

            foreach (var type in types)
            {
                if (type.IsImplementInterface(typeof(IWantToSubscribeThisEventInterface)))
                {
                    RegisterSubscribers(type);
                }
                else if (type.IsImplementInterface(typeof(IHandleMessage)))
                {
                    ChakadContainer.RegisterMessageHandlers(type);
                }
            }

            return configure;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param>
        ///     <name>filterByDll</name>
        /// </param>
        /// <param name="assemblies"></param>
        public static Configure With(this Configure configure, params Assembly[] assemblies)
        {
            var assemblyList = assemblies.ToList();
            var assembliesName = "";
            assemblyList.ForEach(asmbl => assembliesName += $"'{asmbl.FullName}' ");

            Logger.LogSystemLog(EventIdConstants.InitialiingChakad,
              $"Start initializing Chakad with custom custom assemblies ={assembliesName}");

            var types = TypeHelper.GetTypes(assemblyList,
                typeof(IWantToSubscribeThisEventInterface)
                , typeof(IHandleMessage));
            With(configure , types);

            return configure;
        }

        #region ~ Private and Internal Methodes! ~
        private static void RegisterSubscribers(Type type)
        {
            var handler =
                type.GetInterfaces()
                    .ToList()
                    .FirstOrDefault(type1 => type1 != typeof(IWantToSubscribeThisEventInterface));

            if (handler == null || !handler.IsGenericType) return;

            var genericArguments = handler.GetGenericArguments().ToList()
                .Where(type1 => type1.IsImplementInterface(typeof(IDomainEvent))).ToList();

            foreach (var type1 in genericArguments.ToList().Where(type1 => type1 != null))
            {
                ChakadContainer.Register(type, type1);
            }
        }
        #endregion
    }
}
