using System;

namespace Chakad.Pipeline.Core.MessageHandler
{
    public interface IRequestResult : IMessageInterface
    {
        /// <summary>
        /// 
        /// </summary>
        bool Succeeded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Exception  AggregatedExceptions { get; set; }
    }

    public class RequestResult : ChakadMessage, IRequestResult
    {
        public RequestResult()
        {
            Succeeded = true;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Exception AggregatedExceptions { get; set; }
    }
}