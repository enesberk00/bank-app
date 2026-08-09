# Bank Application Web API 🏦

A robust, production-ready Bank Application Web API built with **.NET Core (C#)**, following the principles of **Clean Architecture (N-Tier)**. This API allows users to manage customers, accounts, cards, and perform secure transactions with high performance and reliability.

## 🚀 Key Features & Architectural Decisions

- **Customer & Account Management:** Create customers, open bank accounts, and toggle their Active/Passive states.
- **Card Management & Transactions:** Issue credit cards with risk limits and securely transfer money between accounts and credit cards.
- **Distributed Caching:** Integrated **Redis** to cache frequently accessed data, significantly improving API response times.
- **Security & Protection:** 
  - Fully secured endpoints using **JWT (JSON Web Tokens)**.
  - Configured **Rate Limiting** to protect endpoints against brute-force and DDoS attacks.
- **Robust Validation:** Implemented **FluentValidation** to ensure incoming requests are strictly validated before hitting the business logic.
- **Centralized Error Handling:** Created a Custom **Exception Middleware** to catch unhandled errors globally and return standardized API responses.
- **Clean Database Configurations:** Used **Fluent API** (instead of Data Annotations) to keep Domain Entities pure and map database relationships cleanly.
- **Asynchronous Operations:** Full implementation of **Cancellation Tokens** across all layers to abort long-running tasks gracefully when clients disconnect, optimizing server resources.

## 🛠️ Technologies & Tools

- **Framework:** .NET 6 / 7 / 8
- **Language:** C#
- **Database:** PostgreSQL (Configured via Fluent API)
- **ORM:** Entity Framework Core
- **Architecture:** 4-Layer Architecture (Core, Data, Service, API)
- **Object Mapping:** AutoMapper
- **Validation:** FluentValidation
- **Caching:** Redis
- **Security:** JWT Bearer & Rate Limiting

## 🏗️ Architecture Design (4-Layer)

1. **Core Layer:** Contains Entities, DTOs, and Interfaces (`IRepository`, `IUnitOfWork`). Strictly independent.
2. **Data (Repository) Layer:** Implements Core interfaces. Contains `DbContext`, Fluent API configurations, Generic Repository, and Unit of Work.
3. **Service (Business) Layer:** Contains core business logic, AutoMapper profiles, and FluentValidation rules.
4. **API Layer:** Presentation layer containing Controllers, JWT setup, Rate Limiting, Swagger, and Exception Middleware.

## ⚙️ Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) installed
- [PostgreSQL](https://www.postgresql.org/download/) installed and running
- [Redis](https://redis.io/download) installed and running (or via Docker: `docker run -p 6379:6379 redis`)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/bank-app.git
   cd bank-app
   
2. Configure AppSettings:
   
   Open appsettings.json in the API project.
   
   Update DefaultConnection for PostgreSQL and the Redis connection string.

4. **Apply Migrations:**
    ```bash
   dotnet ef database update --project YourProject.Data --startup-project YourProject.API

4.**Run the Application:**
 ```bash

  dotnet run --project YourProject.API




   

