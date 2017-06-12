using System;

namespace Chakad.Pipeline.Core.Message
{
    public interface IChakadResult : IMessageInterface
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

    public class ChakadResult : ChakadMessage, IChakadResult
    {
        public ChakadResult()
        {
            Succeeded = true;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Exception AggregatedExceptions { get; set; }
    }
}