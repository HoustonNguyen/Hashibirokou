using System;

namespace DiscordCommunications.Exceptions
{
    public class DiscordCommunicationsException : Exception
    {
        public DiscordCommunicationsException() : base()
        {

        }

        public DiscordCommunicationsException(string message) : base(message)
        {

        }

        public DiscordCommunicationsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class DiscordClientInstantiationException : DiscordCommunicationsException
    {
        public DiscordClientInstantiationException() : base()
        {

        }

        public DiscordClientInstantiationException(string message) : base(message)
        {

        }

        public DiscordClientInstantiationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
