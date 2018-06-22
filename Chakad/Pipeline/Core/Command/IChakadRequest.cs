using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Command
{

    public interface IChakadRequest : IMessageInterface
    {
        //TODO
    }
    public interface IChakadRequest<TResponse> : IChakadRequest 
        where TResponse : IChakadResult
    {
    }

    public abstract class ChakadRequest<TResponse> : ChakadMessage, IChakadRequest<TResponse> where TResponse : 
        ChakadResult
    {
        
    }

}