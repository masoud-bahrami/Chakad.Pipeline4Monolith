namespace Chakad.Pipeline.Core.MessageHandler
{

    public interface IRequest : IMessageInterface
    {
        //TODO
    }
    public interface IRequest<TResponse> : IRequest where TResponse : IRequestResult
    {
    }

    public abstract class Request<TResponse> : ChakadMessage, IRequest<TResponse> where TResponse : 
        RequestResult
    {
        
    }

}