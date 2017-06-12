using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Event;
using Chakad.Pipeline.Core.Exceptions;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Pipeline.Core.Options;

namespace Chakad.Pipeline
{
    public class ChakadPipeline : IPipeline
    {
        private static readonly object ObjectLock = new object();
        public async Task Subscribe<T>(IWantToHandleEvent<T> eventHandler, Type type)
            where T : IDomainEvent
        {
            if (type == null || eventHandler == null)
                return;

            var type1 = eventHandler.GetType();

            var key = typeof(T);

            Configure.Register(type1, key);
        }

        public async Task UnSubscribe<T>(IWantToHandleEvent<T> eventHandler, Type myEvent) where T : IDomainEvent
        {
            if (myEvent == null || eventHandler == null)
                return;

            var type1 = eventHandler.GetType();

            var key = typeof(T);

            Configure.UnRegister(type1, key);
        }

        public async Task<TOut> Send<TOut>(IChakadRequest<TOut> command, TimeSpan? timeout = null,
            TaskScheduler _taskScheduler = null, SendOptions options = null) where TOut : ChakadResult
        {

            var commandType = command.GetType();

            var baseType = command.GetType().BaseType;
            if (baseType == null)
                throw new Exception();

            var eventHandler = Configure.ResolveMessageHandler(commandType);

            if (eventHandler == null)
                throw new ChakadPipelineNotFoundHandler(@"Not found handler for {0}", command);

            var instance = ActivatorHelper.CreateNewInstance(eventHandler);

            var tokenSource = new CancellationTokenSource();

            if (_taskScheduler == null)
                _taskScheduler = TaskScheduler.Default;

            if (timeout == null)
                timeout = new TimeSpan(0, 0, 0, 30);

            var task = Task<TOut>.Factory.StartNew(() => InvokeMessageHandle(command, eventHandler, instance),
                tokenSource.Token, TaskCreationOptions.None, _taskScheduler);

            if (task.IsCompleted || task.Wait((int)timeout.Value.TotalMilliseconds, tokenSource.Token))
            {
                return task.Result;
            }

            tokenSource.Cancel();
            throw new ChakadPipelineTimeoutException();
        }

        private static TOut InvokeMessageHandle<TOut>(IChakadRequest<TOut> command, Type eventHandler, object instance)
            where TOut : ChakadResult
        {
            var res = (from info in eventHandler.GetMethods()
                       where info.Name.ToLower() == "handle"
                       select info.Invoke(instance, new object[] { command }))
                .FirstOrDefault();

            var task = res as Task<TOut>;
            return task?.Result;
        }

        public async Task Publish<T>(T myDomainEvent, SendOptions options)
            where T : IDomainEvent
        {
            await Task.Run(() => GetValue(myDomainEvent));
        }

        private static async Task GetValue<T>(T myDomainEvent) where T : IDomainEvent
        {
            var type = myDomainEvent.GetType();

            var eventHandlers = Configure.ResolveEventSubscribers(type);

            var orderOf = OrderConfiger.GetOrderOf(type);

            Parallel.ForEach(orderOf, order =>
                {
                    if (!eventHandlers.Contains(order)) return;
                    var handleDomainEvent = ActivatorHelper.CreateNewInstance<IWantToHandleEvent<T>>(order);
                    handleDomainEvent.Handle(myDomainEvent);
                }
            );

            Parallel.ForEach(eventHandlers.Except(orderOf), newInstance =>
            {
                var wantToHandleEvent = ActivatorHelper.CreateNewInstance<IWantToHandleEvent<T>>(newInstance);
                wantToHandleEvent.Handle(myDomainEvent);
            });
        }
    }
}
