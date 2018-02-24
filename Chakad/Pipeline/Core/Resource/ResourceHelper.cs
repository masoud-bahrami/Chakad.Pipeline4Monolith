using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace Chakad.Pipeline.Core.Resource
{
    public enum ResourceType
    {
        Message
    }
    public class ResourceHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="assembly"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetResourceValue(string key, Assembly assembly = null, params string[] parameters)
        {
            if (assembly == null)
                assembly = typeof(Resource).Assembly;
            var rm = new ResourceManager("Chakad.Pipeline.Core.Resource" + "." + "Resource", assembly);

            try
            {
                var value = rm.GetString(key, Thread.CurrentThread.CurrentCulture);

                if (parameters != null)
                    NormilizeValue(ref value, parameters);
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        private static void NormilizeValue(ref string value, string[] parameters)
        {
            for (var i = 0; i < parameters.Length; i++)
            {
                value = value.Replace($"{{{i}}}", parameters[i]);
            }
        }


        public static string GetResourceFile(Assembly assembly, string resourceFile)
        {
            var resourceStream = assembly.GetManifestResourceStream(assembly.GetName().Name + "." + resourceFile);
            if (resourceStream == null)
                throw new ArgumentNullException();

            var reader = new StreamReader(resourceStream);
            var body = reader.ReadToEnd();
            reader.Close();
            return body;
        }
    }

}
