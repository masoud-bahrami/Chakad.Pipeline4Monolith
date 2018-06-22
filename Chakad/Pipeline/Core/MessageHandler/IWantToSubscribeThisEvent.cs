using Chakad.Pipeline.Core.Event;

namespace Chakad.Pipeline.Core.MessageHandler
{
    public interface IWantToSubscribeThisEvent<in T> : IWantToSubscribeThisEventInterface where T :
        IDomainEvent
    {
        void Handle(T domainEvent);
    }

    public interface IWantToSubscribeThisEventInterface
    {
    }
}