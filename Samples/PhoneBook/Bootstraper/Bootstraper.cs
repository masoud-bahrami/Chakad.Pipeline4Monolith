using System;
using Autofac;
using Chakad.Core;
using Chakad.Pipeline;
using Chakad.Pipeline.Core;
using Chakad.Sample.PhoneBook.Repository;
using Chakad.Samples.PhoneBook.CommandHandlers;
using Chakad.Samples.PhoneBook.Model;
using Chakad.Samples.PhoneBook.QueryHandlers;

namespace Chakad.Samples.PhoneBook.Bootstraper
{
    public static class Bootstraper
    {
        #region ApplicationPath
        private static string _applicationPath;
        private static string ApplicationPath
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationPath))
                    _applicationPath = PathHelper.ExecutionPath;

                return _applicationPath;
            }
        }
        #endregion
        #region IPipeline
        private static IPipeline _pipeline;

        public static IPipeline Pipeline => _pipeline ?? (_pipeline = ServiceLocator<IPipeline>.Resolve());
        #endregion
        #region IQueryEngeen
        private static IQueryEngeen _queryEngeen;
        public static IQueryEngeen QueryEngeen => _queryEngeen ?? (_queryEngeen = ServiceLocator<IQueryEngeen>.Resolve());
        #endregion
        

        public static void Run(bool iNeedSampleData=true)
        {
            RegisterDeendencies(iNeedSampleData);
            ConfigContainer();
            ConfigChakadPipeline();
        }

        private static IContainer ConfigContainer(bool iNeedSampleData = true)
        {
            var container = new ContainerBuilder();
            container.RegisterTypes(typeof (ContactsQueryHandler));
            container.RegisterTypes(typeof (CreateContactHander));
            container.RegisterTypes(typeof (DeleteContactHander));
            container.RegisterTypes(typeof (TestAttributeCommandHander));
            container.RegisterTypes(typeof (UpdateContactHander));
            container.RegisterTypes(typeof(GetContactQueryHandler));
            container.RegisterType<ContactRepository>().As<IContactRepository>()
                .UsingConstructor(() => new ContactRepository(iNeedSampleData))
                .SingleInstance();
            return container.Build();
        }

        #region Private Methodes
        private static void ConfigChakadPipeline()
        {
            var container = ConfigContainer();
            
            Configure.With(ApplicationPath).SetContainer(container);
        }

        private static void RegisterDeendencies(bool iNeedSampleData)
        {
            ServiceLocator<IPipeline>.Register(new ChakadPipeline());
            ServiceLocator<IQueryEngeen>.Register(new ChakadQueryEngeen());
            ServiceLocator<IContactRepository>.Register(new ContactRepository(iNeedSampleData));
        }
        #endregion
    }
}
