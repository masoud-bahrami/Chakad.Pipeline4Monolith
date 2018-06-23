using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Chakad.Container;


namespace Chakavak.Container
{
    public class ChakadContainer : IChakadContainer, IDisposable
    {
        private static ContainerBuilder _containerBuilder;
        private static IContainer _container;

        public ChakadContainer()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public void RegisterType(Type type)
        {
            _containerBuilder.RegisterType(type);
        }

        public void RegisterTypes(params Type[] types)
        {
            _containerBuilder.RegisterTypes(types);
        }

        public IContainer Container => _container;

        public void Dispose()
        {
        }

        internal ChakadContainer CaptureViewModels(Assembly assembly, Func<Type, bool> func)
        {
            var types = assembly.GetTypes();

            foreach (var type in types.Where(func)) _containerBuilder.RegisterType(type).Named(type.Name, type);

            return this;
        }

        internal ChakadContainer RegisterIdentity()
        {
            //_containerBuilder.RegisterType<IdentityService>()
            //  .As<IdentityService>()
            // .SingleInstance();
            return this;
        }
        
        public static T Resolve<T>(string name)
        {
            return _container != null ? _container.ResolveNamed<T>(name) : default(T);
        }

        public static T Resolve<T>()
        {
            return _container != null ? _container.Resolve<T>() : default(T);
        }

        internal static void Build()
        {
            if (_container == null)
                _container = _containerBuilder.Build();
        }
        
    }
}