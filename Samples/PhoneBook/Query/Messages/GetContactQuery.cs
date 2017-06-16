using Chakad.Pipeline.Core.Query;

namespace Chakad.Samples.PhoneBook.Queries
{
    public class GetContactQuery : ChakadQuery<GetContactQueryResult>
    {
        public int Id { get; set; }
    }
    public class GetContactQueryResult : ChakadQueryResult<ContactQueryResult>
    {
    }

    public class ContactQueryResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}