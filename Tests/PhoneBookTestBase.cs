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
        private ICommandPipeline pipeline
        {
            get { return Bootstraper.Pipeline; }
        }
        private IQueryPipeline queryEngeen
        {
            get { return Bootstraper.QueryEngeen; }
        }
        public void Initializer(bool iNeedSampleData = true)
        {
            Bootstraper.Run(iNeedSampleData);
        }

        public async Task<TOut> SendCommand<TOut>(IChakadRequest<TOut> command, TimeSpan? timeout = null,
            Action<Exception, TimeSpan> action = null, SendOptions options = null) where TOut : ChakadResult ,new ()
        {
            var send =  await pipeline.StartProcess(command, timeout, action, options);
            return send;
        }

        public async Task<TOut> RunQuery<TOut> (IBusinessQuery<TOut> query, TimeSpan? timeout = null,
            Action<Exception, TimeSpan> action = null, SendOptions options = null)
            where TOut :class
        {
            var task = await queryEngeen.StartProcess(query, timeout, action, options);
            return task;
        }
    }
}