using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chakad.Core;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Event;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.Pipeline
{
    public class Configure
    {
        #region ~ Private Memebers ! ~
        private static Configure instance;
        private static Dictionary<Type, List<Type>> _eventSubscribers;
        private static Dictionary<Type, Type> _messageHandlersRepository;
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
        /// <param name="handler"></param>
        /// <param name="domainEvent"></param>
        public static Configure Register(Type handler, Type domainEvent)
        {
            Initialize();

            if (!_eventSubscribers.ContainsKey(domainEvent))
                _eventSubscribers.Add(domainEvent, new List<Type>
                {
                    handler
                });
            else
                if (!_eventSubscribers[domainEvent].Contains(domainEvent))
                _eventSubscribers[domainEvent].Add(handler);

            return Instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="domainEvent"></param>
        /// <returns></returns>
        public static Configure UnRegister(Type handler, Type domainEvent)
        {
            Initialize();

            if (_eventSubscribers.ContainsKey(domainEvent))
            {
                _eventSubscribers[domainEvent].Remove(handler);
            }

            return Instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyPath"></param>
        public static Configure With(string assemblyPath)
        {
            var types = TypeHelper.GetTypes(assemblyPath, typeof(IWantToHandleThisEventInterface)
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
                    RegisterMessageHandlers(type);
                }
            }

            return Instance;
        }
        /// <summary>
        /// 
        /// </summary>
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

        internal static List<Type> ResolveEventSubscribers(Type domainEvent)
        {
            Initialize();
            return _eventSubscribers.ContainsKey(domainEvent) ? _eventSubscribers[domainEvent] : null;
        }
        internal static Type ResolveMessageHandler(Type domainEvent)
        {
            Initialize();
            return _messageHandlersRepository.ContainsKey(domainEvent) ? _messageHandlersRepository[domainEvent] : null;
        }
        internal static void Initialize()
        {
            if (_eventSubscribers == null)
                _eventSubscribers = new Dictionary<Type, List<Type>>();

            if (_messageHandlersRepository == null)
                _messageHandlersRepository = new Dictionary<Type, Type>();
        }
        private static void RegisterMessageHandlers(Type type)
        {
            var handler = type.BaseType;

            if (handler == null || !handler.IsGenericType) return;

            var genericArguments = handler.GetGenericArguments()
                .ToList().FirstOrDefault(type1 => type1.IsImplementInterface(typeof(IRequest)));

            if (genericArguments != null)
                _messageHandlersRepository[genericArguments] = type;
        }

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
                Register(type, type1);
            }
        }
        #endregion
    }
}
