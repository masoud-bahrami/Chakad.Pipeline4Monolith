using System;
using System.Threading;
using System.Threading.Tasks;
using Chakad.Messages.Command;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.MessageHandler.CommandHandlers
{
    public class StartActivityCommand : IWantToHandleThisMessage<Messages.Command.StartActivityCommand, StartActivityCommandRequestResult>, IWantToRunAfter<Messages.Command.StartActivityCommand>
    {
        public override async Task<StartActivityCommandRequestResult> Execute(Messages.Command.StartActivityCommand message)
        {
            //TODO
            Console.WriteLine("-----------------" + DateTime.Now + "--------------------");
            Console.WriteLine("ActivityCommandHandler");
            Console.WriteLine("Activity id is: " + message.Id + " Activity message is " + message.Message);

            Console.WriteLine("-----------------" + DateTime.Now + "--------------------");
            Console.WriteLine("Send  AnotherActivityCommand in StartActivityCommand");

            Thread.Sleep(1000);
            var result = Pipeline.Send(new Messages.Command.AnotherActivityCommand
            {
                Message = "message from StartActivityCommandRequestResult",
                ActivityId = message.Id
            });

            var anotherActivityCommand = result;

            return new StartActivityCommandRequestResult
            {
                Id = Guid.NewGuid(),
                Message = "Mehrnoosh Mahyar Masoud",
                Succeeded = true
            };
        }
    }
}