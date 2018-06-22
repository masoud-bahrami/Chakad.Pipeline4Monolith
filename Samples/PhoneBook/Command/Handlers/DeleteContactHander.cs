using System.Threading.Tasks;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Samples.PhoneBook.Commands;
using Chakad.Samples.PhoneBook.Model;

namespace Chakad.Samples.PhoneBook.CommandHandlers
{
    public class DeleteContactHander : IWantToHandleThisCommand<DeleteContact, ChakadResult>
    {
        public IContactRepository ContactRepository;

        public DeleteContactHander(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }
        public override async Task<ChakadResult> Execute(DeleteContact message)
        {
            var contact = ContactRepository.Get(message.Id);

            ContactRepository.Delete(contact);

            return new ChakadResult();
        }

        public override async Task<bool> CheckAccessPolicy(DeleteContact message)
        {
            return true;
        }
    }
}