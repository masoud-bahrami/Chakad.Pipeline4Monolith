using Chakad.Core;
using Chakad.Pipeline;
using Chakad.Pipeline.Core;
using Chakad.Sample.PhoneBook.Repository;
using Chakad.Samples.PhoneBook.Model;

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
        private static ICommandPipeline _pipeline;

        public static ICommandPipeline Pipeline => _pipeline ?? (_pipeline = ServiceLocator<ICommandPipeline>.Resolve());
        #endregion
        #region IQueryEngeen
        private static IQueryPipeline _queryEngeen;
        public static IQueryPipeline QueryEngeen => _queryEngeen ?? (_queryEngeen = ServiceLocator<IQueryPipeline>.Resolve());
        #endregion

        public static void Run(bool iNeedSampleData = true)
        {
            RegisterDeendencies(iNeedSampleData);

            ConfigChakadPipeline(iNeedSampleData);
        }

        #region Private Methodes
        private static void ConfigChakadPipeline(bool iNeedSampleData = true)
        {
            var container = new Container();
            container.RegisterRepository(iNeedSampleData);

            Configure.With(ApplicationPath, "chakad")
                .SetContainer(container);

            Container.Build();
        }

        private static void RegisterDeendencies(bool iNeedSampleData)
        {
            ServiceLocator<ICommandPipeline>.Register(new ChakadCommandPipeline());
            ServiceLocator<IQueryPipeline>.Register(new QueryPipeline());
            ServiceLocator<IContactRepository>.Register(new ContactRepository(iNeedSampleData));
        }
        #endregion
    }
}
