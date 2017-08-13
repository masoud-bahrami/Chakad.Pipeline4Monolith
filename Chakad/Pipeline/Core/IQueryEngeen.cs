using System;
using System.Threading.Tasks;
using Chakad.Pipeline.Core.Options;
using Chakad.Pipeline.Core.Query;

namespace Chakad.Pipeline.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQueryEngeen
    {
        /// <summary>
        /// Sends the provided message.
        /// 
        /// </summary>
        /// <param name="query">The message to send.</param>
        /// <param name="timeout"></param>
        /// <param name="taskScheduler"></param>
        /// <param name="options">The options for the send.</param>
        Task<TOut> Run<TOut>(IBusinessQuery<TOut> query, TimeSpan? timeout = null, Action<Exception, TimeSpan> action = null, SendOptions options = null) where TOut : QueryResult;
    }
}