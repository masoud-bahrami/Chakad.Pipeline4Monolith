using Chakad.Pipeline.Core.Event;

namespace Chakad.Pipeline.Core.MessageHandler
{
    public interface IWantToHandleEvent<in T> : IWantToHandleThisEventInterface where T :
        IDomainEvent
    {
        void Handle(T domainEvent);
    }

    public interface IWantToHandleThisEventInterface
    {
    }
}