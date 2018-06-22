using Chakad.Core;
using Chakad.Pipeline.Core;

namespace Chakad.Pipeline
{
    public static class ChakadServiceBus
    {
        public static ICommandPipeline Pipeline => ServiceLocator<ICommandPipeline>.Resolve();
    }
}
