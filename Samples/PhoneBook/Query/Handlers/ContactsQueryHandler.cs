using System.Linq;
using System.Threading.Tasks;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Samples.PhoneBook.Model;
using Chakad.Samples.PhoneBook.Queries;

namespace Chakad.Samples.PhoneBook.QueryHandlers
{
    public class ContactsQueryHandler : IWantToHandleThisQuery<ContactsQuery, ContactsQueryResult>
    {
        public IContactRepository ContactRepository;

        public ContactsQueryHandler(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }
        public override async Task<ContactsQueryResult> Execute(ContactsQuery message)
        {
            var contacts = ContactRepository.LoadAll();

            if (!string.IsNullOrEmpty(message.SearchText))
                contacts = contacts.Where(contact => contact.LastName.ToLower().Contains(message.SearchText)
                                                        || contact.LastName.ToLower().Contains(message.SearchText)
                                                        || contact.Address.ToLower().Contains(message.SearchText))
                    .ToList();

            var contactQueryResult = new ContactsQueryResult
            {
                TotalCount = contacts.Count,
                Entities = contacts.Select(contact => new ContactQueryResult
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Address = contact.Address
                }).ToList()
            };


            return contactQueryResult;
        }

        public override async Task<bool> CheckAccessPolicy(ContactsQuery message)
        {
            return true;
        }
    }
}