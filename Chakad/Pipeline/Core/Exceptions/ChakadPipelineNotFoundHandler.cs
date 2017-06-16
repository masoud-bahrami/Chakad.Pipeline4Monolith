using System;

namespace Chakad.Pipeline.Core.Exceptions
{
    public class ChakadPipelineNotFoundHandler : Exception
    {
        public ChakadPipelineNotFoundHandler()
            : base()
        { }

        public ChakadPipelineNotFoundHandler(string message)
            : base(message)
        { }

        public ChakadPipelineNotFoundHandler(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        public ChakadPipelineNotFoundHandler(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ChakadPipelineNotFoundHandler(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
            
        }
    }
}