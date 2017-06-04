using System;
using System.Collections.Generic;

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

    }
}