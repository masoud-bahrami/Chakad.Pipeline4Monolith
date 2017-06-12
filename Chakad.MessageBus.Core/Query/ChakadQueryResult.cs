namespace Chakad.Pipeline.Core.Query
{
    public interface ISingleQueryResult : IBusinessQueryResult
    {
    }

    public abstract class ChakadQueryResult<T> : QueryResult, ISingleQueryResult
    {
        public T Entity { get; set; }
    }
}