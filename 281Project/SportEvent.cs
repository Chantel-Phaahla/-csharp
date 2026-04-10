using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEventScheduling
{

    /// Represents a sports-related event, such as a match or tournament.

    public class SportsEvent : Event
    {
        public string SportType { get; private set; } // e.g., Soccer, Basketball, Athletics

        public SportsEvent(string name, DateTime date, string location, int capacity, string sportType)
            : base(name, date, location, capacity)
        {
            if (string.IsNullOrWhiteSpace(sportType))
                throw new ArgumentException("Sport type must be specified.");
            SportType = sportType.Trim();
        }
        /// Override the confirmation message for sports events.
    
        public override string ConfirmEvent()
        {
            return $"Sports Event Confirmed: {Name} ({SportType}) on {Date:d} at {Location}. Capacity: {Capacity}.";
        }

        public override string ToString()
        {
            return base.ToString() + $" | Sport: {SportType}";
        }
    }
}