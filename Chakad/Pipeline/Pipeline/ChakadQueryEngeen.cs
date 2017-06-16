using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Exceptions;
using Chakad.Pipeline.Core.Options;
using Chakad.Pipeline.Core.Query;

namespace Chakad.Pipeline
{
    public class ChakadQueryEngeen : IQueryEngeen
    {
        TOut InvokeMessageHandle<TOut>(IBusinessQuery<TOut> command, Type eventHandler, object instance)
            where TOut : QueryResult
        {
            var res = (from info in eventHandler.GetMethods()
                       where info.Name.ToLower() == "handle"
                       select info.Invoke(instance, new object[] { command }))
                .FirstOrDefault();

            var task = res as Task<TOut>;
            return task?.Result;
        }
        
        public async Task<TOut> Run<TOut>(IBusinessQuery<TOut> query, TimeSpan? timeout=null,
            TaskScheduler taskScheduler=null, SendOptions options=null)
            where TOut : QueryResult
        {
            var commandType = query.GetType();

            var baseType = query.GetType().BaseType;
            if (baseType == null)
                throw new Exception();

            var eventHandler = Configure.ResolveQueryHandler(commandType);

            if (eventHandler == null)
                throw new ChakadPipelineNotFoundHandler(@"Not found handler for {0}", query);

            var instance = ActivatorHelper.CreateNewInstance(eventHandler);

            var tokenSource = new CancellationTokenSource();

            if (taskScheduler == null)
                taskScheduler = TaskScheduler.Default;

            if (timeout == null)
                timeout = new TimeSpan(0, 0, 0, 30);

            var task = Task<TOut>.Factory.StartNew(() =>
            InvokeMessageHandle(query, eventHandler, instance),
                tokenSource.Token, TaskCreationOptions.None, taskScheduler);

            if (task.IsCompleted || task.Wait((int)timeout.Value.TotalMilliseconds, tokenSource.Token))
            {
                return task.Result;
            }

            tokenSource.Cancel();
            throw new ChakadPipelineTimeoutException();
        }
        
    }
}
