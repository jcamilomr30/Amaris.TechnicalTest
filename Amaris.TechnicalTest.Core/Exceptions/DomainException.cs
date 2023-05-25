using System;
using System.Runtime.Serialization;

namespace Amaris.TechnicalTest.Core.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
