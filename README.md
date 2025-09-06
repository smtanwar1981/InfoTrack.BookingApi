# InfoTrack Booking Slot API (ASP.NET Core 8)

A simple **.NET 8 Web API** that manages customer booking slots.  
This API ensures bookings do not overlap, validates working hours (09:00–17:00, last start at 16:00), and limits concurrent requests to improve performance.

---

## 🚀 Features
- Create a new booking slot for a customer.
- Prevents overlapping booking slots.
- Validates requests against business rules:
  - Buyer name cannot be empty.
  - Booking time must be in valid 24-hour format.
  - Bookings allowed only between **09:00 and 17:00** (59-minute slot).
- Concurrency control: API accepts only **4 simultaneous requests**.
- Built with **.NET 8** and **In-Memory Database (EF Core)**.

---

## 🛠️ Tech Stack
- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core InMemory](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli)
- REST API (JSON-based responses)
- Swagger (API documentation)

---

## 📦 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Clone the Repository
```bash
git clone https://github.com/smtanwar1981/InfoTrack.BookingApi.git
cd InfoTrack.BookingApi