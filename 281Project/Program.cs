using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SportsEventScheduling
{
    enum MenuOption
    {
        AddEvent = 1,
        RegisterParticipant,
        RecordPayment,
        ViewSchedule,
        ViewOutstandingFees,
        Exit
    }

    class Program
    {
        private static Scheduler scheduler = new Scheduler();
        private static FileMonitor monitor;
        private static bool running = true;

        static void Main(string[] args)
        {
            string watchPath = Path.Combine(Directory.GetCurrentDirectory(), "Registrations");
            string backupPath = Path.Combine(Directory.GetCurrentDirectory(), "Backups");
            monitor = new FileMonitor(watchPath, backupPath);

            // Hardcoded users
            var users = new Dictionary<string, string>
            {
                { "admin", "1234" },   // gets a 5-day event
                { "user2", "abcd" }    // gets a 25-day event
            };

            Console.WriteLine("=== SPORTS & EVENT SCHEDULING SYSTEM ===");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (!users.ContainsKey(username) || users[username] != password)
            {
                Console.WriteLine("Invalid login. Exiting...");
                return;
            }

            Console.WriteLine($"Welcome, {username}!");

            // Start background processes
            monitor.Start();
            Thread reminderThread = new Thread(ReminderService);
            reminderThread.IsBackground = true;
            reminderThread.Start();

            // User-specific demo events
            if (username == "admin")
            {
                Event demoEvent = new SportsEvent(
                    name: "Admin Soccer Match",
                    date: DateTime.Now.AddDays(5),
                    location: "Community Field",
                    capacity: 10,
                    sportType: "Soccer"
                );
                scheduler.AddEvent(demoEvent);

                Participant demoParticipant = new Participant("John Admin", "admin@example.com", 100m);
                demoEvent.AddParticipant(demoParticipant);

                Console.WriteLine("5-day demo event loaded for Admin.\n");
            }
            else if (username == "user2")
            {
                Event demoEvent = new WorkshopEvent(
                    name: "User2 Coding Workshop",
                    date: DateTime.Now.AddDays(25),
                    location: "Tech Hub",
                    capacity: 20,
                    topic: "C# Basics",
                    speaker: "Jane Doe"
                );
                scheduler.AddEvent(demoEvent);

                Participant demoParticipant = new Participant("Jane User", "user2@example.com", 200m);
                demoEvent.AddParticipant(demoParticipant);

                Console.WriteLine("25-day demo event loaded for User2.\n");
            }

            // Main menu loop with enum
            while (running)
            {
                Console.WriteLine("\n=== SPORTS & EVENT SCHEDULING SYSTEM ===");
                Console.WriteLine("1. Add Event");
                Console.WriteLine("2. Register Participant");
                Console.WriteLine("3. Record Payment");
                Console.WriteLine("4. View Event Schedule");
                Console.WriteLine("5. View Outstanding Fees");
                Console.WriteLine("6. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                try
                {
                    if (Enum.TryParse(choice, out MenuOption option))
                    {
                        switch (option)
                        {
                            case MenuOption.AddEvent:
                                AddEventMenu();
                                break;

                            case MenuOption.RegisterParticipant:
                                RegisterParticipantMenu();
                                break;

                            case MenuOption.RecordPayment:
                                RecordPaymentMenu();
                                break;

                            case MenuOption.ViewSchedule:
                                scheduler.ViewSchedule();
                                break;

                            case MenuOption.ViewOutstandingFees:
                                ViewOutstandingFees();
                                break;

                            case MenuOption.Exit:
                                running = false;
                                Console.WriteLine("Exiting system...");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Try again.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            monitor.Stop();
        }

        // === Background Reminder Service (moved outside Main) ===
        static void ReminderService()
        {
            while (running)
            {
                foreach (var ev in scheduler.GetEvents())
                {
                    TimeSpan timeToEvent = ev.Date - DateTime.Now;

                    if (timeToEvent.TotalDays <= 25 && timeToEvent.TotalDays > 24)
                    {
                        Console.WriteLine($"\n*** Heads-up: Event '{ev.Name}' is in 25 days! Date: {ev.Date} at {ev.Location} ***\n");
                    }
                    else if (timeToEvent.TotalDays <= 5 && timeToEvent.TotalDays > 4)
                    {
                        Console.WriteLine($"\n*** Reminder: Event '{ev.Name}' is in 5 days! Date: {ev.Date} at {ev.Location} ***\n");
                    }
                    else if (timeToEvent.TotalHours <= 24 && timeToEvent.TotalHours > 0)
                    {
                        Console.WriteLine($"\n*** Final Reminder: Event '{ev.Name}' is happening tomorrow! Date: {ev.Date} at {ev.Location} ***\n");
                    }
                }

                Thread.Sleep(60000); // check every 1 minute
            }
        }

        // === Menu Handlers with Hints ===
        static void AddEventMenu()
        {
            Console.Write("Enter event name (e.g., Summer Soccer Cup): ");
            string name = Console.ReadLine();

            Console.Write("Enter date (yyyy-mm-dd, e.g., 2025-09-01): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter location (e.g., Community Field A): ");
            string location = Console.ReadLine();

            Console.Write("Enter capacity (e.g., 20): ");
            int capacity = int.Parse(Console.ReadLine());

            Console.Write("Is this a Sports Event (S) or Workshop (W)? ");
            string type = Console.ReadLine().Trim().ToUpper();

            Event newEvent;
            if (type == "S")
            {
                Console.Write("Enter sport type (e.g., Soccer, Netball): ");
                string sport = Console.ReadLine();
                newEvent = new SportsEvent(name, date, location, capacity, sport);
            }
            else
            {
                Console.Write("Enter workshop topic (e.g., C# Basics): ");
                string topic = Console.ReadLine();
                Console.Write("Enter speaker (e.g., Jane Doe): ");
                string speaker = Console.ReadLine();
                newEvent = new WorkshopEvent(name, date, location, capacity, topic, speaker);
            }

            scheduler.AddEvent(newEvent);
            Console.WriteLine(newEvent.ConfirmEvent());
        }

        static void RegisterParticipantMenu()
        {
            var events = scheduler.GetEvents();
            if (events.Count == 0)
            {
                Console.WriteLine("No events available.");
                return;
            }

            Console.WriteLine("Select event by number:");
            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {events[i]}");
            }

            int eventIndex = int.Parse(Console.ReadLine()) - 1;
            Event selectedEvent = events[eventIndex];

            if (selectedEvent.IsFull)
            {
                Console.WriteLine("Event is full.");
                return;
            }

            Console.Write("Enter participant name (e.g., Michael Smith): ");
            string name = Console.ReadLine();

            Console.Write("Enter contact info (e.g., mike123@gmail.com): ");
            string contact = Console.ReadLine();

            Participant participant = new Participant(name, contact, 0m);
            selectedEvent.AddParticipant(participant);

            Console.WriteLine("Participant registered successfully!");
        }

        static void RecordPaymentMenu()
        {
            var events = scheduler.GetEvents();
            if (events.Count == 0)
            {
                Console.WriteLine("No events available.");
                return;
            }

            Console.WriteLine("Select event by number:");
            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {events[i]}");
            }

            int eventIndex = int.Parse(Console.ReadLine()) - 1;
            Event selectedEvent = events[eventIndex];

            var participants = selectedEvent.GetParticipants();
            if (participants.Count == 0)
            {
                Console.WriteLine("No participants registered for this event.");
                return;
            }

            Console.WriteLine("Select participant by number:");
            for (int i = 0; i < participants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {participants[i]}");
            }

            int participantIndex = int.Parse(Console.ReadLine()) - 1;
            Participant chosen = participants[participantIndex];

            Console.Write("Enter payment amount (e.g., 150.00): ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (amount <= 0)
                throw new InvalidPaymentException("Payment must be greater than zero.");

            chosen.MakePayment(amount);

            Console.WriteLine("Payment recorded successfully!");
        }

        static void ViewOutstandingFees()
        {
            var events = scheduler.GetEvents();
            foreach (var ev in events)
            {
                foreach (var participant in ev.GetParticipants())
                {
                    if (participant.Balance > 0)
                    {
                        Console.WriteLine($"{participant.Name} owes {participant.Balance:C} (Event: {ev.Name})");
                    }
                }
            }
        }
    }
}