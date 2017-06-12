using System;
using Chakad.Pipeline.Core.Query;

namespace Chakad.Messages.Query
{
    public class GetOutgoingFaxQuery : SingleQuery<GetOutgoingFaxQueryResult>
    {
        public Guid Id { get; set; }
    }
    public class GetOutgoingFaxQueryResult : SingleQueryResult<OutgoingFaxResult>
    {
    }

    public class OutgoingFaxResult
    {
        public Guid Id { get; set; }
    }
}