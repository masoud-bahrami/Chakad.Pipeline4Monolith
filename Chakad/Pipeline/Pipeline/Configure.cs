using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chakad.Container;
using Chakad.Core;
using Chakad.Pipeline.Core.Attributes;
using Chakad.Pipeline.Core.Event;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.Pipeline
{
    public class Configure
    {
        #region ~ Private Memebers ! ~
        private static Configure instance;
        /// <summary>Provides static access to the configuration object.</summary>
        public static Configure Instance
        {
            get
            {
                return Configure.instance;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="assemblyNameContain"></param>
        /// <param name="filterByDll"></param>
        public static Configure With(string assemblyPath,string assemblyNameContain = "",bool filterByDll = true)
        {
            var types = TypeHelper.GetTypes(assemblyPath,assemblyNameContain,filterByDll, typeof(IWantToHandleThisEventInterface)
                , typeof(IHandleMessage));
            With(types);
            return Instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        public static Configure With(List<Type> types)
        {
            foreach (var type in types)
            {
                if (type.IsImplementInterface(typeof(IWantToHandleThisEventInterface)))
                {
                    RegisterSubscribers(type);
                }
                else if (type.IsImplementInterface(typeof(IHandleMessage)))
                {
                    ChakadContainer.RegisterMessageHandlers(type);
                }
            }

            return Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param>
        ///     <name>filterByDll</name>
        /// </param>
        /// <param name="assemblies"></param>
        public static Configure With(params Assembly[] assemblies)
        {
            var types = TypeHelper.GetTypes(assemblies.ToList(),
                typeof(IWantToHandleThisEventInterface)
                , typeof(IHandleMessage));
            With(types);

            return Instance;
        }

        #region ~ Private and Internal Methodes! ~

        

        private static void RegisterSubscribers(Type type)
        {
            var handler =
                type.GetInterfaces()
                    .ToList()
                    .FirstOrDefault(type1 => type1 != typeof(IWantToHandleThisEventInterface));

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
