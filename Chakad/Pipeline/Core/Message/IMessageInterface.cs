using Chakad.Pipeline.Core.Internal;
using System;

namespace Chakad.Pipeline.Core.Message
{
    public interface IMessageInterface
    {
        string CorrelationId { get; set; }
    }

    public class ChakadMessage : IMessageInterface
    {
        public string CorrelationId { get; set; }
        public ChakadMessage()
        {
            CorrelationId = CorrelationIdConstants.Command;
        }
    }
}