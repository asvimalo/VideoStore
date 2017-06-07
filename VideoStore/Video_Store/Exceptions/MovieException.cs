using System;
using System.Runtime.Serialization;

namespace Video_Store
{
    [Serializable]
    public class MovieException : Exception
    {
        public MovieException()
        {
        }

        public MovieException(string message) : base(message)
        {
        }

        public MovieException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MovieException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}