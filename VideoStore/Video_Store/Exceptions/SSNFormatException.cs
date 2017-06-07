using System;
using System.Runtime.Serialization;

namespace Video_Store
{
    [Serializable]
    public class SSNFormatException : Exception
    {
        public SSNFormatException()
        {
        }

        public SSNFormatException(string message) : base(message)
        {
        }

        public SSNFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SSNFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}