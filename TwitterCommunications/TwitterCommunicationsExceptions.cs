using System;

namespace TwitterCommunications.Exceptions
{
    public class TwitterCommunicationsException : Exception
    {
        public TwitterCommunicationsException() : base()
        {

        }

        public TwitterCommunicationsException(string message) : base(message)
        {

        }

        public TwitterCommunicationsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class RateLimitException : TwitterCommunicationsException
    {
        public RateLimitException() : base()
        {

        }

        public RateLimitException(string message) : base(message)
        {

        }

        public RateLimitException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
