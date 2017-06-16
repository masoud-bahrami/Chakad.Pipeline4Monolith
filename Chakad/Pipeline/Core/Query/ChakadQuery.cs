namespace Chakad.Pipeline.Core.Query
{
    public interface ISingleQuery<TOut> : IBusinessQuery<TOut> 
        where TOut : ISingleQueryResult
    {
    }

    public class ChakadQuery<TOut> : ISingleQuery<TOut> 
        where TOut : ISingleQueryResult
    {
    }
}