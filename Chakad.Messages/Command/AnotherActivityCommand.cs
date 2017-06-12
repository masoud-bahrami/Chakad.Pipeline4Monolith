using System;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Microsoft.Build.Framework;

namespace Chakad.Messages.Command
{
    public class AnotherActivityCommand : Request<AnotherActivityCommandChakadResult>
    {
        [Required]
        public Guid ActivityId { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class AnotherActivityCommandChakadResult : ChakadResult
    {
        public Guid Id { get; set; }
    }
}
