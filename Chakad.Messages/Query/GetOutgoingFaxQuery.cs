using System;
using Chakad.Pipeline.Core.Query;

namespace Chakad.Messages.Query
{
    public class GetOutgoingFaxQuery : ChakadQuery<GetOutgoingFaxQueryResult>
    {
        public Guid Id { get; set; }
    }
    public class GetOutgoingFaxQueryResult : ChakadQueryResult<OutgoingFaxResult>
    {
    }

    public class OutgoingFaxResult
    {
        public Guid Id { get; set; }
    }
}