using System;
using System.Collections.Generic;
using Chakad.Core;
using Chakad.MessageHandler.EventSubscribers;
using Chakad.Messages.Events;
using Chakad.Pipeline;
using Chakad.Pipeline.Core;

namespace Chakad.Bootstraper
{
    public static class ChakadBootstraper
    {
        public static void Run()
        {
            Console.WriteLine("Start Bootstrap Chakad");
            RegisterServices();
            
            Configure.With(PathHelper.ExecutionPath)
                   .Order(typeof(MyDomainEvent), ConfigOreders());
            Console.WriteLine("End Bootstrap Chakad");
        }

        public static void ReorderEvents()
        {
            Configure.Instance.Order(typeof(MyDomainEvent), ConfigOreders());
        }

        #region ~ Private Mathodes ~
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
