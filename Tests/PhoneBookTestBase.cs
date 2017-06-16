using System;
using System.Threading.Tasks;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.Options;
using Chakad.Pipeline.Core.Query;
using Chakad.Samples.PhoneBook.Bootstraper;

namespace Chakad.Tests
{
    public class PhoneBookTestBase
    {
        private IPipeline pipeline
        {
            get { return Bootstraper.Pipeline; }
        }
        private IQueryEngeen queryEngeen
        {
            get { return Bootstraper.QueryEngeen; }
        }
        public void Initializer(bool iNeedSampleData = true)
        {
            Bootstraper.Run(iNeedSampleData);
        }

        public TOut SendCommand<TOut>(IChakadRequest<TOut> command, TimeSpan? timeout = null,
            TaskScheduler _taskScheduler = null, SendOptions options = null) where TOut : ChakadResult
        {
            var send =  pipeline.Send(command, timeout, _taskScheduler, options);
            return send.Result;
        }

        public TOut RunQuery<TOut> (IBusinessQuery<TOut> query, TimeSpan? timeout = null,
            TaskScheduler taskScheduler = null, SendOptions options = null)
            where TOut :QueryResult
        {
            var task = queryEngeen.Run(query, timeout, taskScheduler, options);
            return task.Result;
        }
    }
}