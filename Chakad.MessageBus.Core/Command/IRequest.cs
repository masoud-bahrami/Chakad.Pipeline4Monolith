using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Command
{

    public interface IRequest : IMessageInterface
    {
        //TODO
    }
    public interface IRequest<TResponse> : IRequest where TResponse : IChakadResult
    {
    }

    public abstract class Request<TResponse> : ChakadMessage, IRequest<TResponse> where TResponse : 
        ChakadResult
    {
        
    }

}