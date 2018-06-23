using System;
using Chakad.Core.Attributes;
using Chakad.Messages.Events;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.MessageHandler.EventSubscribers
{
    [TimeToBeReceived("00:00:10")]
    public class MyEventSubscriber2 : IWantToSubscribeThisEvent<MyDomainEvent>
    {
        public void Handle(MyDomainEvent domainEvent)
        {
            Console.WriteLine("----------------- " + DateTime.Now + " --------------------");
            Console.WriteLine("MyEventSubscriber 2");
            
            Console.WriteLine(domainEvent.FirstName +" "+ domainEvent.LastName);
        }
    }
}
