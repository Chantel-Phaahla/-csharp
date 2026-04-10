using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEventScheduling
{
    
    /// Thrown when a payment operation is invalid (e.g., negative or zero amount).
    
    public class InvalidPaymentException : Exception
    {
        public InvalidPaymentException() { }

        public InvalidPaymentException(string message)
            : base(message) { }

        public InvalidPaymentException(string message, Exception inner)
            : base(message, inner) { }
    }
}