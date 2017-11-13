using System;
using Autofac;

namespace Chakad.Container
{
    public interface IChakadContainer
    {
        void RegisterType(Type type);
        void RegisterTypes(params Type[] types);
        IContainer Container { get; }
    }

    public class DefaultContainer : IChakadContainer
    {
        private readonly ContainerBuilder _containerBuilder;

        public DefaultContainer()
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

        private IContainer _container;

        public IContainer Container
        {
            get
            {
                if (_container == null)
                    _container = _containerBuilder.Build();

                return _container;
            } 
        }
    }
}
