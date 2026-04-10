using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEventScheduling
{
    
    /// Abstract base class representing a generic event.

    /// Custom interface
    public interface IEvent
    {
        Guid Id { get; }
        string Name { get; }
        DateTime Date { get; }
        string Location { get; }
        int Capacity { get; }
        bool IsFull { get; }

        void AddParticipant(Participant p);
        void RemoveParticipant(Guid participantId);
        IReadOnlyList<Participant> GetParticipants();
        string ConfirmEvent();
    }
    //Abstract Event class implements the inteface 
    public abstract class Event : IEvent
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public string Location { get; private set; }
        public int Capacity { get; private set; }
        protected List<Participant> Participants { get; }

        public bool IsFull => Participants.Count >= Capacity;

        protected Event(string name, DateTime date, string location, int capacity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name cannot be empty.");
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

            Id = Guid.NewGuid();
            Name = name.Trim();
            Date = date;
            Location = location.Trim();
            Capacity = capacity;
            Participants = new List<Participant>();
        }

        public virtual void AddParticipant(Participant p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (IsFull) throw new InvalidOperationException("Cannot add participant. Event is full.");
            if (Participants.Any(x => x.Id == p.Id))
                throw new InvalidOperationException("This participant is already registered for the event.");

            Participants.Add(p);
        }

        public virtual void RemoveParticipant(Guid participantId)
        {
            var participant = Participants.FirstOrDefault(x => x.Id == participantId);
            if (participant != null)
                Participants.Remove(participant);
        }

        public IReadOnlyList<Participant> GetParticipants() => Participants.AsReadOnly();

        public abstract string ConfirmEvent();

        public override string ToString()
            => $"{Name} | {Date:d} | {Location} | Capacity: {Capacity}, Registered: {Participants.Count}";
    }
}