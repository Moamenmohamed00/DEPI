# Learn Web API: A Step-by-Step Guide

Welcome to the learning guide for Web API development using ASP.NET Core. This document uses the **ApiSessions** project as a live reference to explain how modern Web APIs are built.

## Project Overview: Task Management API
The project is a simple yet complete Task Management system. It follows the **Service Pattern** and uses **Entity Framework Core (EF Core)** for data persistence.

---

## 1. Project Setup and Configuration
Everything begins in `Program.cs`. This is where you configure services and the HTTP request pipeline.

### Key Components:
- **Dependency Injection (DI)**: Registering services like `AppDbContext` and `TaskService`.
- **Swagger**: Provides a visual UI to test your endpoints (`builder.Services.AddSwaggerGen()`).
- **Middleware**: Configuring how requests are handled (e.g., `app.UseHttpsRedirection()`, `app.UseAuthorization()`).

> [!TIP]
> Notice the comments at the bottom of [Program.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Program.cs#L34-L42). They outline the core flow of building an API.

---

## 2. The Model Layer
Models define the "shape" of your data.
- **File**: [TaskItem.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Model/TaskItem.cs)
- **Concept**: A simple C# class (POCO) that represents a table in the database.

---

## 3. The Data Layer (EF Core)
This layer handles the communication with the database.
- **File**: [AppDbContext.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Data/AppDbContext.cs)
- **Concept**: The `DbContext` acts as a bridge between your C# code and the SQL Server.
- **Learning Step**: Use the Package Manager Console to run `add-migration` and `update-database`.

---

## 4. The Service Layer (Business Logic)
To keep Controllers "thin" and reusable, we move logic into Services.
- **Interface**: [ItaskIService.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Service/ItaskIService.cs)
- **Implementation**: [TaskService.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Service/TaskService.cs)

### Why use an Interface?
Using `ItaskIService` allows for **Loose Coupling**. The Controller doesn't need to know *how* tasks are deleted; it just knows it can call `DeleteAsync`.

> [!WARNING]
> **Learning Opportunity (Bug Alert!)**: Look at `DeleteAsync` in [TaskService.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Service/TaskService.cs#L25-L31). It finds the task but doesn't actually call `_context.tasks.Remove(t)` or `_context.SaveChangesAsync()`. It returns `true` even though no deletion happened!

---

## 5. The Controller Layer (Endpoints)
Controllers handle incoming HTTP requests and return responses.
- **File**: [TaskItemController.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Controllers/TaskItemController.cs)
- **Attributes**:
    - `[ApiController]`: Enables automatic model validation and other API-specific behaviors.
    - `[Route("api/[controller]")]`: Sets the URL path (e.g., `api/TaskItem`).

---

## 6. Advanced Topics in this Project
- **Content Negotiation**: The project uses `AddNewtonsoftJson()` in `Program.cs` to handle complex JSON types.
- **JSON Patch**: The `Patch` method in [TaskItemController.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Controllers/TaskItemController.cs#L161) allows you to update only specific fields of a task.
- **DI Registration**: See how the service is registered as `Scoped` in [Program.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Program.cs#L13).

---

## Summary Checklist
1. **Define Model** (`TaskItem.cs`)
2. **Setup DbContext** (`AppDbContext.cs`)
3. **Configure Connection String** (`appsettings.json`)
4. **Register Services** (`Program.cs`)
5. **Implement Logic** (`TaskService.cs`)
6. **Expose Endpoints** (`TaskItemController.cs`)
