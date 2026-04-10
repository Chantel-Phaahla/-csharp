using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEventScheduling
{
  
    /// Thrown when attempting to add an event that conflicts with an existing event.
    
    public class ScheduleConflictException : Exception
    {
        public ScheduleConflictException() { }

        public ScheduleConflictException(string message)
            : base(message) { }

        public ScheduleConflictException(string message, Exception inner)
            : base(message, inner) { }
    }
}
