using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _281Project
{
    namespace SportsEventScheduling
    {
        
        /// Represents a payment made by a participant or team.
       
        public class Payment
        {
            public Guid Id { get; }
            public Guid ParticipantId { get; }
            public decimal Amount { get; }
            public DateTime Date { get; }
            public string Method { get; } // e.g. "Cash", "Card", "EFT"

            public Payment(Guid participantId, decimal amount, string method)
            {
                if (amount <= 0)
                    throw new ArgumentOutOfRangeException(nameof(amount), "Payment amount must be positive.");
                if (string.IsNullOrWhiteSpace(method))
                    throw new ArgumentException("Payment method must be specified.");

                Id = Guid.NewGuid();
                ParticipantId = participantId;
                Amount = amount;
                Method = method.Trim();
                Date = DateTime.Now;
            }

            public override string ToString()
                => $"Payment {Id}: Participant {ParticipantId}, Amount: {Amount:C}, Method: {Method}, Date: {Date}";
        }
    }
}
