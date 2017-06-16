using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Pipeline.Core.Attributes;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.MessageHandler
{
    [ThisIsTheBaseHandlerForCommandObject]
    public abstract class IWantToHandleThisRequest<T, TOut> :
        MessageHandlerBase<T, TOut>
        where T : class, IChakadRequest<TOut>
        where TOut : class, IChakadResult, new()
    {
        private IPipeline _pipeline;

        protected IPipeline Pipeline => _pipeline ?? (_pipeline = ServiceLocator<IPipeline>.Resolve());

        public  async Task<TOut> Handle(IChakadRequest<TOut> message)
        {
            return await InternalHandle((T)message);
        }
    }
}