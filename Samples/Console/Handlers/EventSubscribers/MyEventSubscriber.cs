using System;
using Chakad.Messages.Events;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.MessageHandler.EventSubscribers
{
    public class MyEventSubscriber : IWantToSubscribeThisEvent<MyDomainEvent>
    {
        public void Handle(MyDomainEvent domainEvent)
        {
            Console.WriteLine("----------------- " + DateTime.Now + " --------------------");
            Console.WriteLine("MyEventSubscriber 0");
            
            Console.WriteLine(domainEvent.FirstName +" " + domainEvent.LastName);
        }
    }
}
