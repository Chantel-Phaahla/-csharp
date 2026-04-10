using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEventScheduling
{

    /// Represents a workshop-style event such as a training session or seminar.
    
    public class WorkshopEvent : Event
    {
        public string Topic { get; private set; }   // e.g., "First Aid Training", "Coaching Workshop"
        public string Speaker { get; private set; } // e.g., name of the trainer/coach

        public WorkshopEvent(string name, DateTime date, string location, int capacity, string topic, string speaker)
            : base(name, date, location, capacity)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Workshop topic must be specified.");
            if (string.IsNullOrWhiteSpace(speaker))
                throw new ArgumentException("Speaker name must be specified.");

            Topic = topic.Trim();
            Speaker = speaker.Trim();
        }

        
        /// Override the confirmation message for workshop events.
    
        public override string ConfirmEvent()
        {
            return $"Workshop Confirmed: {Name} on {Date:d} at {Location}. Topic: {Topic}, Speaker: {Speaker}. Capacity: {Capacity}.";
        }

        public override string ToString()
        {
            return base.ToString() + $" | Topic: {Topic} | Speaker: {Speaker}";
        }
    }
}
