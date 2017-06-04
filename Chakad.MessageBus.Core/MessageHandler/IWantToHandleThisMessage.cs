using System.Threading.Tasks;
using Chakad.Core;

namespace Chakad.Pipeline.Core.MessageHandler
{
    public abstract class IWantToHandleThisMessage<T, TOut> :
        MessageHandlerBase<T, TOut>
        where T : class, IRequest<TOut>
        where TOut : class, IRequestResult, new()
    {
        private IPipeline _pipeline;

        protected IPipeline Pipeline => _pipeline ?? (_pipeline = ServiceLocator<IPipeline>.Resolve());

        public  async Task<TOut> Handle(IRequest<TOut> message)
        {
            return await InternalHandle((T)message);
        }
    }
}