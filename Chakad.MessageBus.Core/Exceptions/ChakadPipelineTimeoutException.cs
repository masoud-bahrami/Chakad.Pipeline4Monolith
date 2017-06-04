using System;

namespace Chakad.Pipeline.Core.Exceptions
{
    public class ChakadPipelineTimeoutException : Exception
    {
        public ChakadPipelineTimeoutException()
            : base()
        { }

        public ChakadPipelineTimeoutException(string message)
            : base(message)
        { }

        public ChakadPipelineTimeoutException(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        public ChakadPipelineTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ChakadPipelineTimeoutException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {

        }
    }
}