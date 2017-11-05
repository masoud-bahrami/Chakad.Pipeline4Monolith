using System.Threading.Tasks;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Samples.PhoneBook.Commands;
using Chakad.Samples.PhoneBook.Model;

namespace Chakad.Samples.PhoneBook.CommandHandlers
{
    public class CreateContactHander : IWantToHandleThisRequest<CreateContact, CreateContactResult>
    {
        public IContactRepository ContactRepository;

        public CreateContactHander()
        {
            
        }
        public CreateContactHander(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }

        public override async Task<CreateContactResult> Execute(CreateContact message)
        {
            var lastId = ContactRepository.GetLastId();
            var contact = new Contact
            {
                FirstName = message.FirstName,
                LastName = message.LastName,
                Address = message.Address,
                Id = ++lastId
            };

            ContactRepository.Add(contact);

            return new CreateContactResult
            {
                Id = contact.Id
            };
        }
    }
}