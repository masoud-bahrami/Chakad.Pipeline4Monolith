using System;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Messages.Command
{
    public class TestCommand : ChakadRequest<ChakadResult>
    {
        public string DocumentName { get; set; }
        public string Note { set; get; }
        public DateTime ScheduleTime { get; set; }
        public string Subject { get; set; }
        public Guid SendFaxTaskId { get; set; }
        public string MachineName { get; set; }
    }
}
