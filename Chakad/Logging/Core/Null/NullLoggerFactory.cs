
namespace Chakad.Logging.Core.Null
{
    public class NullLoggerFactory : ILoggerFactory
    {
        private static NullLoggerFactory instance = new NullLoggerFactory();
        public static NullLoggerFactory Instance { get { return instance; } }
        public void AddProvider(ILoggerProvider provider)
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return NullLoggerProvider.Instance.CreateLogger(categoryName);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
