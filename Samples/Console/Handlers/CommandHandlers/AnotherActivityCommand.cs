using System;
using System.Threading.Tasks;
using Chakad.Messages.Command;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.MessageHandler.CommandHandlers
{
    public class AnotherActivityCommand : IWantToHandleThisRequest<Messages.Command.AnotherActivityCommand,
        AnotherActivityCommandChakadResult>
    {
        public override async Task<AnotherActivityCommandChakadResult> Execute(Messages.Command.AnotherActivityCommand message)
        {
            //TODO
            Console.WriteLine("----------------- "+ DateTime.Now + " --------------------");
            Console.WriteLine("AnotherActivityCommand");
            Console.WriteLine("Start Activity id is: " + message.ActivityId + " Start Activity message is " + message.Message);

            return new AnotherActivityCommandChakadResult
            {
                Id = Guid.NewGuid(),
                Message = "Okay",
                Succeeded = true
            };
        }

        public override async Task<bool> CheckAccessPolicy(Messages.Command.AnotherActivityCommand message)
        {
            return true;
        }
    }
}