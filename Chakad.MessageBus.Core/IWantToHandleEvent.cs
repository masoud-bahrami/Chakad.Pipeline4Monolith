namespace Chakad.Pipeline.Core
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