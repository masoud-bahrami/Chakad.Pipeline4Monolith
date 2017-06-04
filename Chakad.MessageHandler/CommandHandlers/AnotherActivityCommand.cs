using System;
using System.Threading.Tasks;
using Chakad.Messages.Command;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.MessageHandler.CommandHandlers
{
    public class AnotherActivityCommand : IWantToHandleThisMessage<Messages.Command.AnotherActivityCommand,
        AnotherActivityCommandRequestResult>
    {
        public override async Task<AnotherActivityCommandRequestResult> Execute(Messages.Command.AnotherActivityCommand message)
        {
            //TODO
            Console.WriteLine("----------------- "+ DateTime.Now + " --------------------");
            Console.WriteLine("AnotherActivityCommand");
            Console.WriteLine("Start Activity id is: " + message.ActivityId + " Start Activity message is " + message.Message);

            return new AnotherActivityCommandRequestResult
            {
                Id = Guid.NewGuid(),
                Message = "Okay",
                Succeeded = true
            };
        }
    }
}