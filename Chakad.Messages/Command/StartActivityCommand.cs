using System;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;
using Chakad.Pipeline.Core.MessageHandler;
using Microsoft.Build.Framework;

namespace Chakad.Messages.Command
{
    public class StartActivityCommand : Request<StartActivityCommandChakadResult>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class StartActivityCommandChakadResult : ChakadResult
    {
        public Guid Id { get; set; }
    }

    public class StartActivityCommand1 : Request<StartActivityCommandChakadResult>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
