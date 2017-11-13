using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Chakad.Core;
using Chakad.Pipeline.Core.Attributes;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Container
{
    internal class ChakadContainer
    {
        internal static Dictionary<Type, List<Type>> EventSubscribers;
        internal static Dictionary<Type, Type> CommandHandlersRepository;
        internal static Dictionary<Type, Type> QueryHandlersRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="domainEvent"></param>
        public static void Register(Type handler, Type domainEvent)
        {
            Initialize();

            if (!EventSubscribers.ContainsKey(domainEvent))
                EventSubscribers.Add(domainEvent, new List<Type>
                {
                    handler
                });
            else
                if (!EventSubscribers[domainEvent].Contains(domainEvent))
                EventSubscribers[domainEvent].Add(handler);
        }

        internal static List<Type> ResolveEventSubscribers(Type domainEvent)
        {
            Initialize();
            return EventSubscribers.ContainsKey(domainEvent) ? EventSubscribers[domainEvent] : null;
        }

        internal static Type ResolveCommandHandler(Type domainEvent)
        {
            Initialize();
            return CommandHandlersRepository.ContainsKey(domainEvent) ? CommandHandlersRepository[domainEvent] : null;
        }

        internal static Type ResolveQueryHandler(Type domainEvent)
        {
            Initialize();
            return QueryHandlersRepository.ContainsKey(domainEvent) ?
                QueryHandlersRepository[domainEvent] : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="domainEvent"></param>
        /// <returns></returns>
        public static void UnRegister(Type handler, Type domainEvent)
        {
            Initialize();

            if (EventSubscribers.ContainsKey(domainEvent))
            {
                EventSubscribers[domainEvent].Remove(handler);
            }
        }

        internal static void Initialize()
        {
            if (EventSubscribers == null)
                EventSubscribers = new Dictionary<Type, List<Type>>();

            if (CommandHandlersRepository == null)
                CommandHandlersRepository = new Dictionary<Type, Type>();

            if (QueryHandlersRepository == null)
                QueryHandlersRepository = new Dictionary<Type, Type>();

        }

        private static IChakadContainer _container;
        internal static void Run(IChakadContainer container )
        {

            Initialize();

            _container = container;

            _container.RegisterTypes(CommandHandlersRepository.Keys.ToArray());
            _container.RegisterTypes(CommandHandlersRepository.Values.ToArray());

            _container.RegisterTypes(QueryHandlersRepository.Keys.ToArray());
            _container.RegisterTypes(QueryHandlersRepository.Values.ToArray());

            _container.RegisterTypes(EventSubscribers.Keys.ToArray());
            _container.RegisterTypes(EventSubscribers.Values.SelectMany(list => list).ToArray());
        }
        internal static void RegisterMessageHandlers(Type type)
        {
            Initialize();

            var handler = type.BaseType;

            if (handler == null || !handler.IsGenericType) return;

            var genericArguments = handler.GetGenericArguments()
                .ToList().FirstOrDefault(type1 => type1.IsImplementInterface(typeof(IMessageInterface)));

            if (genericArguments == null) return;

            var customAttributes = type.GetCustomAttributes().ToList();

            if (customAttributes == null || !customAttributes.Any())
                return;

            if (customAttributes.Any(attribute => attribute.GetType() == typeof(ThisIsTheBaseHandlerForCommandObjectAttribute)))
                CommandHandlersRepository[genericArguments] = type;

            else if (customAttributes.Any(attribute => attribute.GetType() == typeof(ThisIsTheBaseHandlerForQueryObjectAttribute)))
                QueryHandlersRepository[genericArguments] = type;
        }
        private static readonly string _autofacScopeName = "chakad_pipeline";

        internal static string AutofacScopeName
        {
            get
            {
                return _autofacScopeName;
            }
        }

        internal static ILifetimeScope Autofac
        {
            get { return _container.Container; }
        }
    }
}
