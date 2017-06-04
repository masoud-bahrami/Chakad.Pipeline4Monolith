using System;
using Chakad.Pipeline.Core.MessageHandler;
using Microsoft.Build.Framework;

namespace Chakad.Messages.Command
{
    public class AnotherActivityCommand : Request<AnotherActivityCommandRequestResult>
    {
        [Required]
        public Guid ActivityId { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class AnotherActivityCommandRequestResult : RequestResult
    {
        public Guid Id { get; set; }
    }
}
