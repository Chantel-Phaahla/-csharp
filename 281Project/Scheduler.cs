using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEventScheduling
{
    
    /// Manages scheduling of events, ensures no conflicts, and provides lookup features.

    public class Scheduler
    {
        private readonly List<Event> _events;

        public Scheduler()
        {
            _events = new List<Event>();
        }

        
        /// Adds a new event if no scheduling conflict exists.
     
        public void AddEvent(Event newEvent)
        {
            if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));

            if (CheckConflict(newEvent))
                throw new ScheduleConflictException("Event conflicts with an existing event.");

            _events.Add(newEvent);
        }

        /// <summary>
        /// Returns all scheduled events.
        /// </summary>
        public IReadOnlyList<Event> GetEvents() => _events.AsReadOnly();

        /// <summary>
        /// Finds an event by its Id.
        /// </summary>
        public Event FindEvent(Guid eventId)  // Removed nullable return type '?'
        {
            return _events.FirstOrDefault(e => e.Id == eventId);
        }

        
        /// Check for conflicting date/time and location.
       
        private bool CheckConflict(Event newEvent)
        {
            return _events.Any(e => e.Date == newEvent.Date && e.Location.Equals(newEvent.Location, StringComparison.OrdinalIgnoreCase));
        }

        
        /// Display all upcoming events to the console.
      
        public void ViewSchedule()
        {
            if (_events.Count == 0)
            {
                Console.WriteLine("No events scheduled.");
                return;
            }

            foreach (var ev in _events.OrderBy(e => e.Date))
            {
                Console.WriteLine(ev.ToString());
            }
        }
    }
}