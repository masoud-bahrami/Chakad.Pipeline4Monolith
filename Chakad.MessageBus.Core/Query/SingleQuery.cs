namespace Chakad.Pipeline.Core.Query
{
    public interface ISingleQuery<TOut> : IBusinessQuery<TOut> 
        where TOut : ISingleQueryResult
    {
    }

    public class SingleQuery<TOut> : ISingleQuery<TOut> 
        where TOut : ISingleQueryResult
    {
    }
}