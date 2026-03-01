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

## 6. Error Handling: try-catch & Logger
The `Update` action in [TaskItemController.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Controllers/TaskItemController.cs#L141-L188) is the main example of structured error handling in this project.

### ILogger — Writing Server Logs
`ILogger<TaskItemController>` is injected through the constructor and used to write messages to the server's log output. The **log level** controls how important a message is:

| Method | Level | When to use |
|---|---|---|
| `LogTrace` | Trace | Very detailed, low-level flow (e.g. "operation completed") |
| `LogInformation` | Information | Normal business events (e.g. "starting update for id 5") |
| `LogWarning` | Warning | Something unexpected but recoverable (e.g. item not found) |
| `LogError` | Error | A failure that needs attention |
| `LogCritical` | Critical | A serious failure that may crash the app |

Log levels are configured in [appsettings.json](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/appsettings.json) — you can filter which levels are actually written to the output.

### try-catch — Handling Exceptions
The `Update` method shows the recommended exception-handling pattern:

```csharp
try
{
    _logger.LogInformation($"Starting update for task {id}.");
    var updated = await _taskService.UpdateAsync(id, newTask);
    _logger.LogTrace($"Update for task {id} completed.");
    return Ok(updated);
}
catch (ArgumentException ex)          // specific: bad input data
{
    return BadRequest($"Invalid data for task {id}. {ex.Message}");
}
catch (NullReferenceException ex)     // specific: task not found (null dereferenced in service)
{
    _logger.LogWarning($"Task {id} not found.");
    _logger.LogError("Error updating task {id}.", ex);
    return NotFound($"Task {id} not found. {ex.Message}");
}
catch (Exception ex)                  // generic fallback for anything unexpected
{
    throw new Exception($"Unexpected error for task {id}.", ex);
}
finally                               // always runs, even if an exception was thrown
{
    Console.WriteLine($"Update operation for task {id} completed.");
}
```

**Catch order matters** — always go from **most specific → least specific**:
1. `ArgumentException` (bad data)
2. `NullReferenceException` (null task in `UpdateAsync` — because `oldTask == null` check is commented out in the service)
3. `Exception` (catch-all fallback)

> [!NOTE]
> The comment at line 190 in the controller explains the problem with this approach: if every action needs its own try-catch and logger calls, you are repeating yourself and violating the **Single Responsibility Principle**. The solution is a **global exception-handling middleware** that catches all unhandled exceptions in one place — the next topic to learn.

---

## 7. The Fake Service (In-Memory Testing)
- **File**: [FakeService.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Service/FakeService.cs)
- **Concept**: `FakeService` implements `ItaskIService` exactly like `TaskService`, but stores data in an **in-memory `List<TaskItem>`** instead of a real database.

### Why is this powerful?
Because both services implement the same interface, you can **swap implementations with a single line** in `Program.cs` — zero changes anywhere else:

```csharp
// Real database:
builder.Services.AddScoped<ItaskIService, TaskService>();

// In-memory (no DB needed!):
builder.Services.AddScoped<ItaskIService, FakeService>();
```

This is the key payoff of the **Interface + DI** pattern: the Controller never knows (or cares) which implementation it gets.

> [!TIP]
> Use `FakeService` during early development or when you don't have a database ready. Switch to `TaskService` when you want real persistence.

---

## 8. DI Service Lifetimes
ASP.NET Core's DI container supports three service lifetimes. The `life time/` folder and `LifeTimeController` exist purely to demonstrate this live.

| Lifetime | Registration | New instance created... | Use when... |
|---|---|---|---|
| **Transient** | `AddTransient<T>()` | Every time it is requested | Stateless, lightweight services |
| **Scoped** | `AddScoped<T>()` | Once per HTTP request | DB contexts, per-request state |
| **Singleton** | `AddSingleton<T>()` | Once for the whole app | Shared config, in-memory caches |

### See it live — `GET /api/LifeTime`
Each service class ([TransientService.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/life%20time/TransientService.cs), [ScopedService.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/life%20time/ScopedService.cs), [SingletonService.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/life%20time/SingletonService.cs)) stores a `Guid id` created at construction time. [LifeTimeController.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Controllers/LifeTimeController.cs) injects **two instances of each** and returns their IDs side-by-side:

```json
{
  "transient": { "id1": "aaa...", "id2": "bbb...", "same": false },
  "scoped":    { "id1": "ccc...", "id2": "ccc...", "same": true  },
  "singleton": { "id1": "ddd...", "id2": "ddd...", "same": true  }
}
```

- **Transient** → always **different** — new object per injection.
- **Scoped** → **same within one request**, different on the next request.
- **Singleton** → **always the same** for the entire app lifetime.

Registration in [Program.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Program.cs#L19-L21):
```csharp
builder.Services.AddTransient<TransientService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddSingleton<SingletonService>();
```

---

## 9. Advanced Topics in this Project
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
7. **Swap Implementations** (`FakeService` vs `TaskService` via DI)
8. **Understand Lifetimes** (`TransientService`, `ScopedService`, `SingletonService`)
9. **Handle Errors** (`ILogger` + try-catch in `TaskItemController.cs`)
10. *(Next)* **Global Exception Middleware** (handle all exceptions in one place)
