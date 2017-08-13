using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Chakad.Core;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Exceptions;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Pipeline.Core.Options;
using Chakad.Pipeline.Core.Query;
using Polly;
using Polly.Retry;

namespace Chakad.Pipeline
{
    public class ChakadQueryEngeen : IQueryEngeen
    {
        async Task<TOut> InvokeMessageHandle<TOut>(IBusinessQuery<TOut> command, Type eventHandler,
            object instance)
            where TOut : QueryResult
        {
            var res = (from info in eventHandler.GetMethods()
                       where info.Name.ToLower() == "handle"
                       select info.Invoke(instance, new object[] { command }))
                .FirstOrDefault();

            var task = res as Task<TOut>;
            return task?.Result;
        }

        public async Task<TOut> Run<TOut>(IBusinessQuery<TOut> query, TimeSpan? timeout = null,
            Action<Exception, TimeSpan> action = null, SendOptions options = null)
            where TOut : QueryResult
        {
            var commandType = query.GetType();

            var baseType = query.GetType().BaseType;
            if (baseType == null)
                throw new Exception();

            var eventHandler = Configure.ResolveQueryHandler(commandType);

            if (eventHandler == null)
                throw new ChakadPipelineNotFoundHandler(@"Not found handler for {0}", query);

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
                    result = await InvokeMessageHandle(query, eventHandler, handler);
                    //result = await (Task<TOut>) concreteType.GetMethod("Handle").Invoke(handler, new object[] {query});
                });
                return result;
            }

        }
    }
}
