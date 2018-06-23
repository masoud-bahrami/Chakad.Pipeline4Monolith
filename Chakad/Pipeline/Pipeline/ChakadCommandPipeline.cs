using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Chakad.Container;
using Chakad.Core.Extensions;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Event;
using Chakad.Pipeline.Core.Exceptions;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Pipeline.Core.Options;
using Chakad.Pipeline.Core.Query;
using Polly;

namespace Chakad.Pipeline
{
    public class ChakadCommandPipeline : ICommandPipeline
    {
        public async Task Subscribe<T>(IWantToSubscribeThisEvent<T> eventHandler, Type type)
            where T : IDomainEvent
        {
            if (type == null || eventHandler == null)
                return;

            var type1 = eventHandler.GetType();

            var key = typeof(T);

            ChakadContainer.Register(type1, key);
        }

        public async Task UnSubscribe<T>(IWantToSubscribeThisEvent<T> eventHandler, Type myEvent) where T : IDomainEvent
        {
            if (myEvent == null || eventHandler == null)
                return;

            var type1 = eventHandler.GetType();

            var key = typeof(T);

            ChakadContainer.UnRegister(type1, key);
        }

        public async Task<TOut> StartProcess<TOut>(IChakadRequest<TOut> command, TimeSpan? timeout = null,
            Action<Exception, TimeSpan> action = null, SendOptions options = null) 
            where TOut : ChakadResult, new()
        {
            var commandType = command.GetType();

            var baseType = command.GetType().BaseType;
            if (baseType == null)
                throw new Exception();

            var eventHandler = ChakadContainer.ResolveCommandHandler(commandType);

            if (eventHandler == null)
                throw new ChakadPipelineNotFoundHandler(@"Not found handler for {0}", command);

            if (timeout == null)
                timeout = new TimeSpan(0, 0, 0, 0, 500);

            if (action == null)
            {
                action = (ex, time) =>
                {
                    //TODO log ex
                    Console.WriteLine(ex.ToString());
                };
            }

            var policy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(1, retryAttempt => timeout.Value, action);

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
            where TOut : ChakadResult, new()
        {
            var res = (from info in eventHandler.GetMethods()
                       where info.Name.ToLower() == "handle"
                       select info.Invoke(instance, new object[] { command }))
                .FirstOrDefault();

            var task1 = res as Task<TOut>;

            if (task1 == null) return null;

            if (task1.Exception != null)
                return new TOut
                {
                    Succeeded = false,
                    AggregatedExceptions = task1.Exception,
                    Message = task1.Exception.GetaAllMessages()
                };

            var task = await task1;
            return task;
        }

        public async Task Publish<T>(T myDomainEvent, SendOptions options)
            where T : IDomainEvent
        {
            await Task.Run(() => PublishThisEvent(myDomainEvent));
        }

        private static async Task PublishThisEvent<T>(T domainEvent) where T : IDomainEvent
        {
            var type = domainEvent.GetType();

            var eventHandlers = ChakadContainer.ResolveEventSubscribers(type);

            var orderOf = OrderConfiger.GetOrderOf(type);

            Parallel.ForEach(orderOf, async order =>
                {
                    if (!eventHandlers.Contains(order))
                        return;

                    using (var scope = ChakadContainer.Autofac.BeginLifetimeScope(ChakadContainer.AutofacScopeName))
                    {
                        var handler = scope.ResolveOptional(order);

                        var concreteType = typeof(IWantToSubscribeThisEvent<>).MakeGenericType(order);

                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { domainEvent });
                    }
                }
            );

            Parallel.ForEach(eventHandlers.Except(orderOf), async newInstance =>
            {
                using (var scope = ChakadContainer.Autofac.BeginLifetimeScope(ChakadContainer.AutofacScopeName))
                {
                    var handler = scope.ResolveOptional(newInstance);

                    var concreteType = typeof(IWantToSubscribeThisEvent<>).MakeGenericType(newInstance);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { domainEvent });
                }
            });
        }
    }
}
