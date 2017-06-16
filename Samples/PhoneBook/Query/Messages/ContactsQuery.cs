using Chakad.Pipeline.Core.Query;

namespace Chakad.Samples.PhoneBook.Queries
{
    public class ContactsQuery : ChakadListQuery<ContactsQueryResult>
    {
    }
    public class ContactsQueryResult : ChakadListQueryResult<ContactQueryResult>
    {
    }

}