using System.Threading.Tasks;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Chakad.Samples.PhoneBook.Commands;

namespace Chakad.Samples.PhoneBook.CommandHandlers
{
    public class TestAttributeCommandHander : IWantToHandleThisCommand<TestAttributeCommand, ChakadResult>
    {
        public override async Task<ChakadResult> Execute(TestAttributeCommand message)
        {
            return new ChakadResult();

        }

        public override async Task<bool> CheckAccessPolicy(TestAttributeCommand message)
        {
            return true;
        }
    }
}