using System.Collections.Generic;

namespace Chakad.Core
{
    public static class ServiceLocator<T>
    {
        private static Dictionary<string, T> Services { get; set; }
        private const string Default="default";
        public static void Register(string key, T provider)
        {
            Initializer();

            if (Services.ContainsKey(key.Trim()))
                Services[key.Trim()] = provider;

            else
                Services.Add(key.Trim(), provider);
        }

        public static void Register(T provider)
        {
            Initializer();

            if (Services.ContainsKey(Default))
                Services[Default] = provider;

            else
                Services.Add(Default, provider);
        }

        public static void Register(string assembly ,string fullName)
        {
            Initializer();

            var newInstance = ActivatorHelper.CreateNewInstance<T>(assembly, fullName);

            Register(newInstance);
        }

        public static T Resolve(string key)
        {
            Initializer();

            return Services[key.Trim()];
        }

        private static void Initializer()
        {
            if (Services == null)
                Services = new Dictionary<string, T>();
        }

        public static T Resolve()
        {
            Initializer();
            if(Services.ContainsKey(Default))
                return Services[Default];
            return default(T);
        }
    }
}
