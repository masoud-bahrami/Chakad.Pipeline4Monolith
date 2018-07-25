using System;
using Chakad.Pipeline.Core.Internal;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Event
{
    /// <summary>
    /// Marker interface to indicate that a class is a event message
    /// 
    /// </summary>
    public interface IDomainEvent : IMessage
    {
    }
    public abstract class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            CorrelationId = CorrelationIdConstants.Event;
        }

        public string CorrelationId { get; set; }
    }
}
