using System;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

using Microsoft.Build.Framework;

namespace Chakad.Messages.Command
{
    public class StartActivityCommand : ChakadRequest<StartActivityCommandChakadResult>
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

    public class StartActivityCommand1 : ChakadRequest<StartActivityCommandChakadResult>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
