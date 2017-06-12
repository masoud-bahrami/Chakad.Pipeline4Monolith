using System.Threading.Tasks;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.MessageHandler
{
    public abstract class MessageHandlerBase<T, TOut> : 
        IHandleMessage
        where T : class, IMessageInterface
        where TOut : class, IChakadResult, new()
    {
        public IPipeline Pipeline { get; set; }

        public virtual void Handle(T message)
        {
            //TODO some processing goes here
        }
        
        public abstract Task<TOut> Execute(T message);
        
        protected async Task<TOut> InternalHandle(T message)
        {
            //TODO Orhestration message... for example

            Handle(message);

            var execute = await Execute(message);
            return execute;
        }
    }
}