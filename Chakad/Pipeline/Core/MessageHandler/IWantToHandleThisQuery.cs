using System.Threading.Tasks;
using Chakad.Core;
using Chakad.Pipeline.Core.Attributes;
using Chakad.Pipeline.Core.Query;

namespace Chakad.Pipeline.Core.MessageHandler
{
    [ThisIsTheBaseHandlerForQueryObject]
    public abstract class IWantToHandleThisQuery<T, TOut>
        : MessageHandlerBase<T, TOut>
        where T : class, IBusinessQuery<TOut>
        where TOut : 
        //QueryResult, 
        class , new()
    {
        private ICommandPipeline _pipeline;

        protected ICommandPipeline Pipeline => _pipeline ?? (_pipeline = ServiceLocator<ICommandPipeline>.Resolve());

        public async Task<TOut> Handle(IBusinessQuery<TOut> message)
        {
            return await InternalHandle((T)message);
        }

    }
}