using System;
using Chakad.Pipeline.Core;

namespace Chakad.Pipeline
{
    public static class EventHandlerExtenssions
    {
        public static void InterestedIn<T>(this IWantToHandleEvent<T> eventHandler, Type @event)
            where T : IDomainEvent
        {
            ChakadServiceBus.Pipeline.Subscribe(eventHandler,@event);
        }
        public static void ReluctanceTo<T>(this IWantToHandleEvent<T> eventHandler, Type @event)
            where T : IDomainEvent
        {
            ChakadServiceBus.Pipeline.UnSubscribe(eventHandler,@event);
        }
        
    }
}