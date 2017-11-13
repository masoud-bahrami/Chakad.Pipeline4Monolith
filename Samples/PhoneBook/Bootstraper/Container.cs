using System;
using Autofac;
using Chakad.Container;
using Chakad.Sample.PhoneBook.Repository;
using Chakad.Samples.PhoneBook.Model;

namespace Chakad.Samples.PhoneBook.Bootstraper
{
    class Container : IChakadContainer
    {
        private static ContainerBuilder _containerBuilder;
        private static IContainer _container;
        public Container(ContainerBuilder containerBuilder = null)
        {
            _containerBuilder = containerBuilder ?? new ContainerBuilder();
        }

        public void RegisterType(Type type)
        {
            _containerBuilder.RegisterType(type);
        }

        public void RegisterTypes(params Type[] types)
        {
            _containerBuilder.RegisterTypes(types);
        }

        internal static void Build()
        {
            if (_container == null)
                _container = _containerBuilder.Build();
        }

        IContainer IChakadContainer.Container
        {
            get { return _container; }
        }

        public void RegisterRepository(bool iNeedSampleData = true)
        {
            _containerBuilder.RegisterType<ContactRepository>().As<IContactRepository>()
                .UsingConstructor(() => new ContactRepository(iNeedSampleData))
                .SingleInstance();
        }
    }
}
