using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Query
{
    public interface ISingleQueryResult : IBusinessQueryResult
    {
    }

    public abstract class SingleQueryResult<T> : QueryResult, ISingleQueryResult
    {
        public T Entity { get; set; }
    }
    public abstract class QueryResult : ChakadResult, IBusinessQueryResult
    {
    }
}