using Chakad.Pipeline.Core.Event;

namespace Chakad.Messages.Events
{
    public class MyDomainEvent : DomainEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
