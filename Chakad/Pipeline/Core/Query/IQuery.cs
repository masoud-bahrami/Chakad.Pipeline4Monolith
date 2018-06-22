using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Query
{
    public interface IQuery : IMessageInterface
    {
        //TODO
    }

    public interface IQuery<TResponse> : IQuery 
    {
    }

    public abstract class Query<TResponse> : ChakadMessage, IQuery<TResponse> where TResponse :
        ChakadResult
    {

    }
}