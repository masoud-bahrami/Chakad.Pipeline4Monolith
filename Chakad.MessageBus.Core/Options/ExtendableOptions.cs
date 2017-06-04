
using System.Collections.Generic;

namespace Chakad.Pipeline.Core.Options
{
    /// <summary>Provide a base class for extendable options.</summary>
    public abstract class ExtendableOptions
    {
        internal string UserDefinedMessageId { get; set; }

        internal Dictionary<string, string> OutgoingHeaders { get; }

        /// <summary>Creates an instance of an extendable option.</summary>
        protected ExtendableOptions()
        {
            
            this.OutgoingHeaders = new Dictionary<string, string>();
        }
    }
}
