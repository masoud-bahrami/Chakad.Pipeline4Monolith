using System;

namespace Chakad.Core.Attributes
{
    /// <summary>
    /// Attribute to indicate that a message has a period of time
    ///             in which to be received.
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class TimeToBeReceivedAttribute : Attribute
    {
        private readonly TimeSpan _timeToBeReceived = TimeSpan.MaxValue;

        /// <summary>
        /// Gets the maximum time in which a message must be received.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// If the interval specified by the TimeToBeReceived property expires before the message
        ///             is received by the destination of the message the message will automatically be cancelled.
        /// 
        /// </remarks>
        public TimeSpan TimeToBeReceived
        {
            get
            {
                return this._timeToBeReceived;
            }
        }

        /// <summary>
        /// Sets the time to be received to be unlimited.
        /// 
        /// </summary>
        public TimeToBeReceivedAttribute()
        {
        }

        /// <summary>
        /// Sets the time to be received.
        /// 
        /// </summary>
        /// <param name="timeSpan">A timespan that can be interpreted by <see cref="M:System.TimeSpan.Parse(System.String)"/>.</param>
        public TimeToBeReceivedAttribute(string timeSpan)
        {
            this._timeToBeReceived = TimeSpan.Parse(timeSpan);
        }
    }
}
