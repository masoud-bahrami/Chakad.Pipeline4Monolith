using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Chakad.Core;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Event;
using Chakad.Pipeline.Core.Exceptions;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Pipeline.Core.Options;
using Polly;
using Polly.Retry;

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
            Action<Exception, TimeSpan> action = null, SendOptions options = null) where TOut : ChakadResult
        {

            var commandType = command.GetType();

            var baseType = command.GetType().BaseType;
            if (baseType == null)
                throw new Exception();

            var eventHandler = Configure.ResolveCommandHandler(commandType);

            if (eventHandler == null)
                throw new ChakadPipelineNotFoundHandler(@"Not found handler for {0}", command);

            if (timeout == null)
                timeout = new TimeSpan(0, 0, 0, 30);

            if (action == null)
            {
                action = (ex, time) =>
                {
                    //TODO log ex
                    Console.WriteLine(ex.ToString());
                };
            }

            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => timeout.Value, action);

            using (var scope = ChakadContainer.Autofac.BeginLifetimeScope(ChakadContainer.AutofacScopeName))
            {

                var handler = scope.ResolveOptional(eventHandler);
                
                TOut result = null;

                await policy.ExecuteAsync(async () =>
                {
                    result = await InvokeMessageHandle(command, eventHandler, handler);
                });
                return result;
            }
        }

        private async Task<TOut> InvokeMessageHandle<TOut>(IChakadRequest<TOut> command, Type eventHandler, object instance)
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
            await Task.Run(() => PublishThisEvent(myDomainEvent));
        }

        private static async Task PublishThisEvent<T>(T domainEvent) where T : IDomainEvent
        {
            var type = domainEvent.GetType();

            var eventHandlers = Configure.ResolveEventSubscribers(type);

            var orderOf = OrderConfiger.GetOrderOf(type);

            Parallel.ForEach(orderOf, async order =>
                {
                    if (!eventHandlers.Contains(order))
                        return;

                    using (var scope = ChakadContainer.Autofac.BeginLifetimeScope(ChakadContainer.AutofacScopeName))
                    {
                        var handler = scope.ResolveOptional(order);

                        var concreteType = typeof(IWantToHandleEvent<>).MakeGenericType(order);

                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { domainEvent });
                    }
                }
            );

            Parallel.ForEach(eventHandlers.Except(orderOf), async newInstance =>
            {
                using (var scope = ChakadContainer.Autofac.BeginLifetimeScope(ChakadContainer.AutofacScopeName))
                {
                    var handler = scope.ResolveOptional(newInstance);

                    var concreteType = typeof(IWantToHandleEvent<>).MakeGenericType(newInstance);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { domainEvent });
                }
            });
        }
    }
}
