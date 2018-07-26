using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Chakad.Container;
using Chakad.Logging;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Exceptions;
using Chakad.Pipeline.Core.Options;
using Chakad.Pipeline.Core.Query;
using Polly;
using Chakad.Logging.Extensions;
using Chakad.Core.Extensions;
using Chakad.Pipeline.Core.Internal;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline
{
    public class QueryPipeline : IQueryPipeline
    {
        private readonly string loggerCategoryName = "Chakad.Query.Pipeline";
        private ILogger Logger
        {
            get
            {
                return LoggerBuilder.Instance.LoggerFactory.CreateLogger(loggerCategoryName);
            }
        }

        async Task<TOut> InvokeMessageHandle<TOut>(IBusinessQuery<TOut> query, Type eventHandler,
            object instance)
            where TOut : class
        {
            Logger.LogInformation(EventIdConstants.QueryStartInvokinMessageHandle, $"InvokeMessageHandle. Correlation_Id is ={query.CorrelationId} Start Invoking Message Handler.");

            var res = (from info in eventHandler.GetMethods()
                       where info.Name.ToLower() == "handle"
                       select info.Invoke(instance, new object[] { query }))
                .FirstOrDefault();

            var task1 = res as Task<TOut>;

            if (task1 == null) return null;

            if (task1.Exception != null)
            {
                Logger.LogError(EventIdConstants.QueryInvokingMessageHandleWasFaield, task1.Exception, $"InvokeMessageHandle. Correlation_Id is ={query.CorrelationId} Error in Invoking Message Handle.");

                return new ChakadResult
                {
                    Succeeded = false,
                    AggregatedExceptions = task1.Exception,
                    Message = task1.Exception.GetaAllMessages()
                } as TOut;
            }
            var task = await task1;

            Logger.LogInformation(EventIdConstants.QueryInvokingMessageHandleWasSuccessfully, $"InvokeMessageHandle.Correlation_Id is ={query.CorrelationId} Message Handler invoked successfully.");
            return task;
        }
        public async Task<TOut> StartProcess<TOut>(IBusinessQuery<TOut> query, TimeSpan? timeout = null,
            Action<Exception, TimeSpan> action = null, SendOptions options = null)
            where TOut : class
        {
            var queryType = query.GetType();
            Logger.LogInformation(EventIdConstants.QueryStartProcess, queryType.FullName, query, $"Start Process query with Correlation_Id = {query.CorrelationId}");

            var baseType = query.GetType().BaseType;
            if (baseType == null)
            {
                Logger.LogError(EventIdConstants.QueryBaseTypeIsEmpty, query.CorrelationId, $"StartProcess. Correlation_Id is ={query.CorrelationId} Query base type is null.");

                throw new Exception();
            }

            var eventHandler = ChakadContainer.ResolveQueryHandler(queryType);

            if (eventHandler == null)
            {
                var exeption = new ChakadPipelineNotFoundHandler(@"Not found handler for {0}", query.CorrelationId);

                Logger.LogError(EventIdConstants.QueryNotFoundHandler, exeption, query.CorrelationId,
                    $"StartProcess. Correlation_Id is ={query.CorrelationId}. Not found handler");
                throw exeption;
            }

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
            Logger.LogInformation(EventIdConstants.QueryInitializeCircuteBreaker,
                query.CorrelationId,
                $"Start Process. Correlation_Id is ={query.CorrelationId}  Set Retry Count equals to 2 in case of any failures in invoking message handler.");


            using (var scope = ChakadContainer.Autofac.BeginLifetimeScope(ChakadContainer.AutofacScopeName))
            {
                var handler = scope.ResolveOptional(eventHandler);

                TOut result = null;

                await policy.ExecuteAsync(async () =>
                {
                    result = await InvokeMessageHandle(query, eventHandler, handler);
                });
                return result;
            }

        }
    }
}
