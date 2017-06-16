using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Event
{
    /// <summary>
    /// Marker interface to indicate that a class is a event message
    /// 
    /// </summary>
    public interface IDomainEvent: IMessage
    {
    }
}
