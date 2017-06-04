using Chakad.Pipeline.Core;

namespace Chakad.Messages.Events
{
    public class MyDomainEvent :IDomainEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
