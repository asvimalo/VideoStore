using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Video_Store
{
    [Serializable]
    public class RentalException : Exception
    {
        private List<Rental> tooLateRented;

        public RentalException()
        {
        }

        public RentalException(string message) : base(message)
        {
        }

        public RentalException(List<Rental> tooLateRented)
        {
            this.tooLateRented = tooLateRented;
        }

        public RentalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RentalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}