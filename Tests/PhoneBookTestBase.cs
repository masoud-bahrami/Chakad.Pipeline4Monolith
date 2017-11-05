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

        public async Task<TOut> SendCommand<TOut>(IChakadRequest<TOut> command, TimeSpan? timeout = null,
            Action<Exception, TimeSpan> action = null, SendOptions options = null) where TOut : ChakadResult
        {
            var send =  await pipeline.Send(command, timeout, action, options);
            return send;
        }

        public async Task<TOut> RunQuery<TOut> (IBusinessQuery<TOut> query, TimeSpan? timeout = null,
            Action<Exception, TimeSpan> action = null, SendOptions options = null)
            where TOut :QueryResult
        {
            var task = await queryEngeen.Run(query, timeout, action, options);
            return task;
        }
    }
}