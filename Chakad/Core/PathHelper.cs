#region

using System;
using System.IO;
using System.Reflection;

#endregion

namespace Chakad.Core
{
    public static class PathHelper
    {
        public static string ExecutionPath
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}