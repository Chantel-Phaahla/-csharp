using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SportsEventScheduling
{
    
    /// Stores participant details and a running balance (amount owed > 0).
  
    public class Participant
    {
        public Guid Id { get; }
        private string _name;
        private string _contact;
        private decimal _balance; // positive means owes money

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value.Trim();
            }
        }

        public string Contact
        {
            get => _contact;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Contact cannot be empty.");
                _contact = value.Trim();
            }
        }

      
        /// Amount outstanding (>= 0). Use MakePayment to reduce.
      
        public decimal Balance => _balance;

        // Constructors
        public Participant(string name, string contact, decimal openingBalance = 0m)
        {
            Id = Guid.NewGuid();
            Name = name;
            Contact = contact;
            if (openingBalance < 0) throw new ArgumentOutOfRangeException(nameof(openingBalance), "Opening balance cannot be negative.");
            _balance = openingBalance;
        }

        // Behaviours
     
        /// Increase the balance (e.g., new fee assigned).
      
        public void Charge(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Charge amount must be positive.");
            _balance += amount;
        }

        
        /// Logs a payment and reduces the outstanding balance.
        
        public void MakePayment(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Payment amount must be positive.");
            // If overpayment occurs, clamp to zero and ignore extra 
            _balance = Math.Max(0, _balance - amount);
        }

        public override string ToString()
            => $"{Name} | Contact: {Contact} | Outstanding: {Balance:C}";
    }
}