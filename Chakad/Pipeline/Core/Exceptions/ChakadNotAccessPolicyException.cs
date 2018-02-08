using System;

namespace Chakad.Pipeline.Core.Exceptions
{
    public class ChakadNotAccessPolicyException : Exception
    {
        public ChakadNotAccessPolicyException()
            : base()
        { }

        public ChakadNotAccessPolicyException(string message)
            : base(message)
        { }

        public ChakadNotAccessPolicyException(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        public ChakadNotAccessPolicyException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ChakadNotAccessPolicyException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {

        }
    }
}