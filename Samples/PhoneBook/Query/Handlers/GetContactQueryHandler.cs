using System.Threading.Tasks;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Samples.PhoneBook.Model;
using Chakad.Samples.PhoneBook.Queries;

namespace Chakad.Samples.PhoneBook.QueryHandlers
{
    public class GetContactQueryHandler : IWantToHandleThisQuery<GetContactQuery, ContactQueryResult>
    {
        public IContactRepository ContactRepository;

        public GetContactQueryHandler(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }
        public override async Task<ContactQueryResult> Execute(GetContactQuery message)
        {
            var contact = ContactRepository.Get(message.Id);

            return new ContactQueryResult
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Address = contact.Address
            };
        }

        public override async Task<bool> CheckAccessPolicy(GetContactQuery message)
        {
            return true;
        }
    }
}