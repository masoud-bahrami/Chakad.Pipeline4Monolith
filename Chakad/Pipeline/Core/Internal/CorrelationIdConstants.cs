using System;

namespace Chakad.Pipeline.Core.Internal
{
    internal class CorrelationIdConstants
    {
        internal static string Command
        {
            get
            {
                return $"cmd02000-{Guid.NewGuid()}";
            }
        }
        internal static string Event
        {
            get
            {
                return $"ent03000-{Guid.NewGuid()}";
            }
        } 
        internal static string Query
        {
            get
            {
                return $"qry04000-{Guid.NewGuid()}";
            }
        } 
    }
}
