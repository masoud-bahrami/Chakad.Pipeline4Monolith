using System;

namespace Chakad.Pipeline.Core.Exceptions
{
    public class ChakadMessageViolationPolicyException : Exception
    {
        public ChakadMessageViolationPolicyException()
            : base()
        { }

        public ChakadMessageViolationPolicyException(string message)
            : base(message)
        { }

        public ChakadMessageViolationPolicyException(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        public ChakadMessageViolationPolicyException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ChakadMessageViolationPolicyException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {

        }
    }
}