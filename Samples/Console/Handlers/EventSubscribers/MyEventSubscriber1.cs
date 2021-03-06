﻿using System;
using Chakad.Messages.Events;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.MessageHandler.EventSubscribers
{
    public class MyEventSubscriber1 : IWantToSubscribeThisEvent<MyDomainEvent>
    {
        public void Handle(MyDomainEvent domainEvent)
        {
            Console.WriteLine("----------------- " + DateTime.Now + " --------------------");
            Console.WriteLine("MyEventSubscriber 1");
            
            Console.WriteLine(domainEvent.FirstName +" "+ domainEvent.LastName);
        }
    }
}
