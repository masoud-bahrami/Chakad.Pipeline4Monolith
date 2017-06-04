using Chakad.MessageBus.Core;

namespace Chakad.MessageHandler
{
    public interface IEventHandler
    {
        void Subscribe(IDomainEvent domainEvent);
    }
}
