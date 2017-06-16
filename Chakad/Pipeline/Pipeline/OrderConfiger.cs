using System;
using System.Collections.Generic;
using System.Linq;

namespace Chakad.Pipeline
{
    /// <summary>
    /// Used to specify the order in which message handlers will be activated.
    /// 
    /// </summary>
    internal static class OrderConfiger
    {
        private static readonly Dictionary<Type, List<Type>> OrderedSubscribers = new Dictionary<Type, List<Type>>();

        /// <summary>
        /// Specifies that the given type will be activated before all others.
        /// 
        /// </summary>
        /// <typeparam name="T"/>
        internal static void SetOrder(Type dommainEvent, List<Type> domainEventHandlers)
        //where T : IDomainEvent
        //where THandler : IWantToHandleEvent<T>
        {
            if (OrderedSubscribers.ContainsKey(dommainEvent))
            {
                foreach (var domainEventHandler in
                        domainEventHandlers.Where(type => !OrderedSubscribers[dommainEvent].Contains(type)))
                {

                    OrderedSubscribers[dommainEvent].Add(domainEventHandler);
                }
            }

            else
            {
                OrderedSubscribers[dommainEvent] = new List<Type>(domainEventHandlers);
            }
        }

        internal static List<Type> GetOrderOf(Type domainEvent)
        //where T : IDomainEvent
        {
            return OrderedSubscribers.ContainsKey(domainEvent) ?
                OrderedSubscribers[domainEvent] : new List<Type>();
        }
    }
}