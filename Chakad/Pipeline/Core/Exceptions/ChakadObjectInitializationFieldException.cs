using System;

namespace Chakad.Pipeline.Core.Exceptions
{
    public class ChakadObjectInitializationFieldException : Exception
    {
        public ChakadObjectInitializationFieldException()
            : base()
        { }

        public ChakadObjectInitializationFieldException(string message)
            : base(message)
        { }

        public ChakadObjectInitializationFieldException(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        public ChakadObjectInitializationFieldException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ChakadObjectInitializationFieldException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {

        }
    }
}