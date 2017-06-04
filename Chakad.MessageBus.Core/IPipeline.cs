﻿using System;
using System.Threading.Tasks;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Pipeline.Core.NewFolder1;

namespace Chakad.Pipeline.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="myEvent"></param>
        Task Subscribe<T>(IWantToHandleEvent<T> eventHandler, Type myEvent)
            where T : IDomainEvent;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="myEvent"></param>
        Task UnSubscribe<T>(IWantToHandleEvent<T> eventHandler, Type myEvent)
            where T : IDomainEvent;
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="myDomainEvent"></param>
        Task Publish<T>(T myDomainEvent, SendOptions options=null) where T: IDomainEvent;

        /// <summary>
        /// Sends the provided message.
        /// 
        /// </summary>
        /// <param name="command">The message to send.</param>
        /// <param name="options">The options for the send.</param>
        Task<TOut> Send<TOut>(IRequest<TOut> command, TimeSpan? timeout = null,
            TaskScheduler _taskScheduler = null, SendOptions options = null) where TOut : RequestResult;

        ///// <summary>
        ///// Sends the provided message.
        ///// 
        ///// </summary>
        ///// <param name="message">The message to send.</param><param name="options">The options for the send.</param>
        //Task Send(object message, SendOptions options);

        ///// <summary>
        ///// Instantiates a message of type T and sends it.
        ///// 
        ///// </summary>
        ///// <typeparam name="T">The type of message, usually an interface.</typeparam><param name="messageConstructor">An action which initializes properties of the message.</param><param name="options">The options for the send.</param>
        //Task Send<T>(Action<T> messageConstructor, SendOptions options);

        ///// <summary>
        ///// Publish the message to subscribers.
        ///// 
        ///// </summary>
        ///// <param name="message">The message to publish.</param><param name="options">The options for the publish.</param>
        //Task Publish(object message, PublishOptions options);

        ///// <summary>
        ///// Instantiates a message of type T and publishes it.
        ///// 
        ///// </summary>
        ///// <typeparam name="T">The type of message, usually an interface.</typeparam><param name="messageConstructor">An action which initializes properties of the message.</param><param name="publishOptions">Specific options for this event.</param>
        //Task Publish<T>(Action<T> messageConstructor, PublishOptions publishOptions);
        
    }
}