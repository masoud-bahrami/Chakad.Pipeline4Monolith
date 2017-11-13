using System;
using System.Collections.Generic;
using Autofac;
using Chakad.Container;

namespace Chakad.Pipeline
{
    public static class ConfigureExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handles"></param>
        /// <returns></returns>
        public static Configure Order(this Configure configure, Type type, List<Type> handles)
        //where T : IDomainEvent 
        //where THandler : IWantToHandleEvent<T>
        {
            OrderConfiger.SetOrder(type, handles);
            return configure;
        }


        public static Configure SetContainer(this Configure configure, IChakadContainer container)
        {
            ChakadContainer.Run(container);
            return configure;
        }
        public static Configure SetDefaultContainer(this Configure configure)
        {
            ChakadContainer.Run(new DefaultContainer());
            return configure;
        }
    }
}