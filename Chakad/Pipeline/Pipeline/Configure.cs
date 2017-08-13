using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
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
        private static Dictionary<Type, List<Type>> _eventSubscribers;
        private static Dictionary<Type, Type> _commandHandlersRepository;
        private static Dictionary<Type, Type> _queryHandlersRepository;
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
            Initialize();

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
        internal static Type ResolveCommandHandler(Type domainEvent)
        {
            Initialize();
            return _commandHandlersRepository.ContainsKey(domainEvent) ? _commandHandlersRepository[domainEvent] : null;
        }
        internal static Type ResolveQueryHandler(Type domainEvent)
        {
            Initialize();
            return _queryHandlersRepository.ContainsKey(domainEvent) ? 
                _queryHandlersRepository[domainEvent] : null;
        }
        internal static void Initialize()
        {
            if (_eventSubscribers == null)
                _eventSubscribers = new Dictionary<Type, List<Type>>();

            if (_commandHandlersRepository == null)
                _commandHandlersRepository = new Dictionary<Type, Type>();

            if (_queryHandlersRepository == null)
                _queryHandlersRepository = new Dictionary<Type, Type>();

        }
        private static void RegisterMessageHandlers(Type type)
        {
            var handler = type.BaseType;

            if (handler == null || !handler.IsGenericType) return;

            var genericArguments = handler.GetGenericArguments()
                .ToList().FirstOrDefault(type1 => type1.IsImplementInterface(typeof(IMessageInterface)));

            if (genericArguments == null) return;

            var customAttributes = type.GetCustomAttributes().ToList();

            if (customAttributes == null || !customAttributes.Any())
                return;

            if (customAttributes.Any(attribute => attribute.GetType() == typeof(ThisIsTheBaseHandlerForCommandObjectAttribute)))
                _commandHandlersRepository[genericArguments] = type;

            else if (customAttributes.Any(attribute => attribute.GetType() == typeof(ThisIsTheBaseHandlerForQueryObjectAttribute)))
                _queryHandlersRepository[genericArguments] = type;
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
