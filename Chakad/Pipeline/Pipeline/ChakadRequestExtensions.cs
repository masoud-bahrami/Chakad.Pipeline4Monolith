using System;
using System.Threading.Tasks;
using Chakad.Pipeline.Core.Event;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.Pipeline
{
    public static class ChakadRequestExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="myEvent"></param>
        public static async Task InterestedIn<T>(this IWantToHandleEvent<T> eventHandler, Type type)
            where T : IDomainEvent
        {
            if (type == null || eventHandler == null)
                return;

            var type1 = eventHandler.GetType();

            var key = typeof(T);

            Configure.Register(type1, key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="myEvent"></param>
        public static async Task DivorceFrom<T>(this IWantToHandleEvent<T> eventHandler, Type myEvent)
            where T : IDomainEvent
        {
            if (myEvent == null || eventHandler == null)
                return;

            var type1 = eventHandler.GetType();

            var key = typeof(T);

            Configure.UnRegister(type1, key);
        }
    }
}
