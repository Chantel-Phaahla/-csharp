# #csharp
# -csharp
# 🏟️ Sports Event Scheduling System (C# Console Application)

## 📌 Project Overview
The **Sports Event Scheduling System** is a C# console application designed to manage and schedule events such as sports matches and workshops. The system allows users to create events, register participants, record payments, view event schedules, and view outstanding fees.

This project also includes file monitoring functionality for registration files, and a reminder service that runs in the background using threading.

---

## 👩‍💻 My Contribution (Group Project)
My main contribution was the development of the Event.cs file, which includes:

- Creating the custom interface IEvent
- Implementing the abstract base class Event
- Implementing core event functionality such as:
  - Adding participants
  - Removing participants
  - Managing event capacity (IsFull)
  - Returning participant lists
  - Defining the abstract method `ConfirmEvent()

This file serves as the foundation for the event types used throughout the project.

---

## 🛠️ Technologies Used
- C# (.NET Console Application)
- Object-Oriented Programming (OOP)
- Interfaces
- Abstract Classes
- Polymorphism
- Exception Handling
- File Handling & FileSystemWatcher
- Threading (Background Reminder Service)
- Collections (List)

---

## 🎯 System Features
- User login system using a dictionary of hardcoded credentials
- Add and schedule events (Sports or Workshop events)
- Register participants for events
- Prevent duplicate participant registration
- Prevent registration when event capacity is full
- Record payments and update participant balances
- Display scheduled events
- View outstanding fees for participants
- Background reminder service using a custom thread to notify users about the event dates

---

## 📂 Key Files
- Program.cs – Main menu system and application logic
- Event.cs – Interface and abstract base class (my contribution)
- SportsEvent.cs – Sports event implementation
- Scheduler.cs – Event scheduling and conflict checking
- Participant.cs – Participant details and payment handling
- FileMonitor.cs – File monitoring and backup system
- InvalidPaymentException.cs – Custom payment exception
- ScheduleConflictException.cs – Custom scheduling conflict exception

---

## ⚙️ How to Run the Project
1. Clone or download the repository  
2. Open the project in Visual Studio  
3. Run the console application  
4. Log in using one of the demo accounts:

### Demo Login Credentials
- **Username:** admin  
  **Password:** 1234

- **Username:** user2 
  **Password:** abcd

5. Use the menu options to interact with the system.

---

## 📌 Menu Options
1. Add Event  
2. Register Participant  
3. Record Payment  
4. View Event Schedule  
5. View Outstanding Fees  
6. Exit  

---

## 📸 Screenshots (Required)


1. **Login Screen**  
- Login.png
- 
3. **Add Event Process**
   - Add Event (2).png

4. **Register Participant**
   - Register participant.png

5. **Record Payment**
   - Record payment.png

6. **View Event Schedule**
   - View Outstanding fees.png

7. **Outstanding Fees Output**
   - View Outstanding fees.png
---

## 🧠 Concepts Demonstrated
- Interfaces (`IEvent`)
- Abstract classes (`Event`)
- Polymorphism (`ConfirmEvent()` overridden in `SportsEvent`)
- Encapsulation (private setters, validation rules)
- Custom Exceptions (`InvalidPaymentException`, `ScheduleConflictException`)
- Threading (ReminderService running as a background thread)
- File Monitoring (`FileSystemWatcher`)
- Collections and LINQ (`List`, `Any`, `FirstOrDefault`, `OrderBy`)

---

## 📌 Notes
This project was completed as part of a group assessment and demonstrates practical implementation of scheduling logic, participant management, file handling, and OOP principles in C#.
