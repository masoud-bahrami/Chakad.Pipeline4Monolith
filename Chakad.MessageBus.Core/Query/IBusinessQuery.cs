using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Query
{
    public interface IBusinessQuery : IMessageInterface
    {
    }
    public interface IBusinessQuery<T> : IBusinessQuery, IQuery<T> where T : IBusinessQueryResult
    {
    }

    public interface IBusinessQueryResult : IChakadResult
    {
    }
}