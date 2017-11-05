using Chakad.Pipeline.Core.Event;

namespace Chakad.Messages.Events
{
    public class MyDomainEvent :IDomainEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
