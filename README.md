# LAAuto

LAAuto is a layered ASP.NET Core web application for managing car service listings and appointments.  
The system allows users to browse services, book appointments, and interact with categorized service data, while administrators manage the platform.

## Architecture
- **LAAuto.Entities** – Database entities and relationships
- **LAAuto.Services** – Service interfaces (business contracts)
- **LAAuto.Services.Impl** – Business logic implementations
- **LAAuto.Web** – ASP.NET Core MVC web application
- **LAAuto.Tests** – Unit tests for services and controllers

## Core Functionality
- Car service management
- Appointment booking
- Category and subcategory support
- User management and permissions
- Administrative operations

## Technologies
- C#
- ASP.NET Core MVC
- Entity Framework (Code First)
- Microsoft SQL Server
- HTML, CSS, JavaScript

## Running the Application
1. Configure SQL Server connection string in `appsettings.json`
2. Apply EF migrations
3. Set `LAAuto.Web` as startup project
4. Run the solution

## Getting Started

1. Install .NET SDK and Microsoft SQL Server.
2. Configure the database connection string in `appsettings.json`.
3. Run the project via Visual Studio or dotnet run.
