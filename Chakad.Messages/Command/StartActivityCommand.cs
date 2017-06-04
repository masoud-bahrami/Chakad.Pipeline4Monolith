using System;
using Chakad.Pipeline.Core.MessageHandler;
using Microsoft.Build.Framework;

namespace Chakad.Messages.Command
{
    public class StartActivityCommand : Request<StartActivityCommandRequestResult>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class StartActivityCommandRequestResult : RequestResult
    {
        public Guid Id { get; set; }
    }

    public class StartActivityCommand1 : Request<StartActivityCommandRequestResult>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
