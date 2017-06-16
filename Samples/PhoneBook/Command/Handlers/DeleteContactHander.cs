using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Samples.PhoneBook.Commands;
using Chakad.Samples.PhoneBook.Model;

namespace Chakad.Samples.PhoneBook.CommandHandlers
{
    public class DeleteContactHander : IWantToHandleThisRequest<DeleteContact, ChakadResult>
    {
        public IContactRepository ContactRepository => ServiceLocator<IContactRepository>.Resolve();

        public override async Task<ChakadResult> Execute(DeleteContact message)
        {
            var contact = ContactRepository.Get(message.Id);

            ContactRepository.Delete(contact);

            return new ChakadResult();
        }
    }
}