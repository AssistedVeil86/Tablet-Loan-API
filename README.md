# Tablet Loaning System API

> A modern Web API backend for managing tablet loans at the library of Universidad Evangélica de El Salvador, built with .NET 10 and Vertical Slice Architecture

## 📋 Overview

This is a Web API backend project built with C# 14 and .NET 10 using Minimal APIs, following **Vertical Slice Architecture**, designed for the tablet loaning system of the **Library of the Universidad Evangélica de El Salvador**.

Developed over the course of a semester, this API is intended for active use during the university's operating hours. Beyond its practical purpose, the project served as a strong exercise to reinforce backend development skills and problem solving with real-world constraints.

## 🚀 Technologies Used

- **.NET 10** — Latest .NET framework
- **C# 14** — Modern C# features
- **ASP.NET Core** — High-performance web framework
- **PostgreSQL** — Robust relational database
- **Entity Framework Core** — ORM for data access
- **Refit** — Type-safe HTTP client for external API communication
- **SignalR** — Real-time communication with physical clients
- **Hangfire** — Background job scheduling
- **OpenAPI / Scalar** — API documentation
- **Cloudflare Turnstile** — Bot protection and API security

## ✨ Features

The core purpose of this API is to administer how tablets are loaned within the library, handling device availability clearly and keeping proper control over each loan's status — whether it is **ongoing** or **completed**. This gives administrators a clear and real-time view of which tablets are available and which loans are active or have ended successfully.

Here's a full breakdown of its capabilities:

### 📱 Tablet Software Management via AirDroid Parental Control
Integration with **AirDroid Parental Control** to restrict what students can and cannot do on the devices, such as blocking access to certain websites and preventing app installation. **Refit** is used to call AirDroid's API endpoints to handle `SignIn`, `SignOut`, `Block`, and `Unblock` operations on the devices.

### ⏰ Automated Device Blocking with Hangfire
**Hangfire Background Jobs** are used to automatically schedule the blocking of a tablet **1 hour after a loan has started**, ensuring devices are properly restricted after the allowed usage window.

### 🔐 Scheduled AirDroid Authentication
Hangfire is also used to schedule the daily **AirDroid SignIn** at the start of the university's operating hours, storing the resulting JWT in a **Singleton** so the entire application has access to it. **SignOut** is equally scheduled once operating hours are over.

### 📋 Loan Creation & Management
When a student scans their ID, a new loan is created with an **Ongoing** status and the tablet blocking is automatically scheduled for 1 hour later. Once the loan ends, the tablet's availability is updated and the loan status is set to **Completed**.

### 🔔 Real-Time Device Release with SignalR
**SignalR** is used to communicate in real-time with physical clients — in this case **Raspberry Pis** — to trigger the physical release of a tablet device whenever a loan is initiated.

### 🛡️ API Protection with Cloudflare Turnstile
The API is protected with **Cloudflare Turnstile** to prevent unauthorized access and misuse, ensuring only legitimate requests from valid clients are processed.

## 🏗️ Building Process

The development process was exciting, as it represented a real challenge that required learning and working with several technologies that were new for me at the time, such as **Hangfire** for background job scheduling and **Refit** for type-safe external API communication.

Beyond the new tools, this project was also a great opportunity to polish and improve the approach to **Vertical Slice Architecture**, incorporating:

- `EntityConfiguration` classes for clean EF Core setup
- The **Result Pattern** for expressive and exception-free error handling
- A more organized and concise structure for **Minimal API endpoints**
- **Extension Methods** for clean service registration and configuration
- Proper **DTO ↔ Entity mapping** in both directions

## 🔧 Areas for Improvement

- **Unit Testing**: Due to time constraints, unit tests were not implemented — despite being absolutely essential to any production-ready project. Adding comprehensive test coverage remains a pending priority.

## 🚀 How to Run the Project

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) installed on your machine
- PostgreSQL database server
- An IDE of your choice:
  - Visual Studio
  - JetBrains Rider
  - Visual Studio Code

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/tablet-loaning-api.git
   cd tablet-loaning-api
   ```

2. **Open the project**
   - Open the `.slnx` file with your preferred IDE

3. **Configure the database**
   - Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=tabletloaning;Username=your_user;Password=your_password"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the project**
   ```bash
   dotnet run
   ```

6. **Access the API documentation**
   - Navigate to the Scalar UI to explore all available endpoints

And that's it — the project is now up and running! 🎉