using System;
using Chakad.Pipeline.Core.MessageHandler;

namespace Chakad.Messages.Command
{
    public class TestCommand : Request<RequestResult>
    {
        public string DocumentName { get; set; }
        public string Note { set; get; }
        public DateTime ScheduleTime { get; set; }
        public string Subject { get; set; }
        public Guid SendFaxTaskId { get; set; }
        public string MachineName { get; set; }
    }
}
