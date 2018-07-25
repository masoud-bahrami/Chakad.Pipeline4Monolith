using System;
using System.Collections.Generic;
using Chakad.Core;
using Chakad.MessageHandler.EventSubscribers;
using Chakad.Messages.Events;
using Chakad.Pipeline;
using Chakad.Pipeline.Core;
using Chakavak.Container;

namespace Chakad.Bootstraper
{
    public static class ChakadBootstraper
    {
        private static ChakadContainer _chakavakContainer;

        public static void Run()
        {
            Console.WriteLine("Start Bootstraping");
            RegisterServices();
            ConfigContainer();
            ConfigChakadPipeline();
            BuildContainer();
            Console.WriteLine("Bootstraping was finished");
        }

        public static void ReorderEvents()
        {
            Configure.Instance.Order(typeof(MyDomainEvent), ConfigOreders());
        }

        #region ~ Private Mathodes ~
        private static void ConfigChakadPipeline()
        {
            Configure.Instance
                .SetDefaultLoggerFactory()
                .SetDefaultLoggerProviders()
                .With(PathHelper.ExecutionPath)
                .Order(typeof(MyDomainEvent), ConfigOreders())
                .SetContainer(_chakavakContainer);
        }
        private static void ConfigContainer()
        {
            _chakavakContainer = new ChakadContainer()
                .RegisterIdentity();
        }
        private static void BuildContainer()
        {
            ChakadContainer.Build();
        }
        private static void RegisterServices()
        {
            ServiceLocator<ICommandPipeline>.Register(new ChakadCommandPipeline());
        }
        private static async void RegisterEventSubscribers()
        {
            await new MyEventSubscriber1().InterestedIn(typeof(MyDomainEvent));
            await new MyEventSubscriber2().InterestedIn(typeof(MyDomainEvent));
            await new MyEventSubscriber().InterestedIn(typeof(MyDomainEvent));
        }
        private static List<Type> ConfigOreders()
        {
            var handles = new List<Type>
            {
                typeof(MyEventSubscriber2),
                typeof(MyEventSubscriber1)
            };
            return handles;
        }
        #endregion
    }
}
