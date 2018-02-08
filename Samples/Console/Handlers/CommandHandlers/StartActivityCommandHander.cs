using System;
using System.Threading;
using System.Threading.Tasks;
using Chakad.Messages.Command;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.MessageHandler.CommandHandlers
{
    public class StartActivityCommandHander : IWantToHandleThisRequest<StartActivityCommand, StartActivityCommandChakadResult>, IWantToRunAfter<StartActivityCommand>
    {
        public override async Task<StartActivityCommandChakadResult> Execute(StartActivityCommand message)
        {
            //TODO
            Console.WriteLine("-----------------" + DateTime.Now + "--------------------");
            Console.WriteLine("ActivityCommandHandler");
            Console.WriteLine("Activity id is: " + message.Id + " Activity message is " + message.Message);

            Console.WriteLine("-----------------" + DateTime.Now + "--------------------");
            Console.WriteLine("Send  AnotherActivityCommand in StartActivityCommandHander");

            Thread.Sleep(1000);
            var result = Pipeline.Send(new Messages.Command.AnotherActivityCommand
            {
                Message = "message from StartActivityCommandChakadResult",
                ActivityId = message.Id
            });

            var anotherActivityCommand = result;

            return new StartActivityCommandChakadResult
            {
                Id = Guid.NewGuid(),
                Message = "Mehrnoosh Mahyar Masoud",
                Succeeded = true
            };
        }

        public override async Task<bool> CheckAccessPolicy(StartActivityCommand message)
        {
            return true;
        }
    }
}