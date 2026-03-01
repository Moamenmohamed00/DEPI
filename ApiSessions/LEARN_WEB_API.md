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
- **Middleware**: Software components that sit in the HTTP pipeline — they run on every request *before* the controller and on every response *after* it.

> [!TIP]
> Notice the comments at the bottom of [Program.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Program.cs#L105-L115). They outline the core flow of building an API.

### Middleware — Three Ways to Write It
Middleware runs in the order it is added. Each component calls `next(context)` to pass control forward.

#### 1. Inline (Lambda) — `app.Use()`
The simplest approach, written directly in `Program.cs`:
```csharp
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    Console.WriteLine($">>>Request: {context.Request.Method} {context.Request.Path}");
    await next(context);  // call the next middleware / controller
    Console.WriteLine($"<<<Response: {context.Response.StatusCode}");
});
```
Other lambda variants:
- **`app.Run()`** — terminal middleware, **no** `next` call. Ends the pipeline.
- **`app.Map("/path", ...)`** — branches the pipeline for a specific URL.

#### 2. Class-Based (Custom Class) — [CustomMiddleware.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Middleware/CustomMiddleware.cs)
A standalone class with a `RequestDelegate _next` injected via constructor:
```csharp
public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    public CustomMiddleware(RequestDelegate next) { _next = next; }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine(">>>Custom Middleware: Before next middleware");
        await _next(context);
        Console.WriteLine("<<<Custom Middleware: After next middleware");
    }
}
```
Registered with an **extension method** for clean `Program.cs` usage:
```csharp
public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
    => builder.UseMiddleware<CustomMiddleware>();

// In Program.cs:
app.UseCustomMiddleware();
```

#### 3. `IMiddleware` Interface — [TestMiddleware.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Middleware/TestMiddleware.cs)
Implementing `IMiddleware` means the class is managed by the DI container (must be registered as a service):
```csharp
public class TestMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Console.WriteLine(">>>Test Middleware: Before next middleware");
        await next(context);
        Console.WriteLine("<<<Test Middleware: After next middleware");
    }
}
```
Registration — **two steps** required:
```csharp
// 1. Register as a service (because IMiddleware uses DI)
builder.Services.AddTransient<TestMiddleware>();

// 2. Add to pipeline
app.UseTestMiddleware();
```

| Approach | Class needed | DI registration | Use when... |
|---|---|---|---|
| `app.Use()` inline | ✗ | ✗ | Quick logging/debugging |
| Class-based | ✓ | ✗ | Reusable, no DI dependencies |
| `IMiddleware` | ✓ | ✓ | Needs injected services (e.g., logger, DB) |

#### The Full Request Lifecycle
The comments in [Program.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Program.cs#L46-L65) include a real server log showing exactly what happens for `PUT /api/TaskItem/65`:
```
>>>Request: PUT /api/TaskItem/65          ← inline app.Use fires first
info:  Starting update operation for id 65 ← LogInformation in controller
info:  Executed DbCommand (47ms)           ← EF Core queries DB
warn:  Task with id 65 not found           ← LogWarning in catch block
fail:  NullReferenceException              ← LogError
crit:  Critical error for task 65          ← LogCritical
Update operation completed.                ← finally block
<<<Response: 404                           ← inline app.Use fires on the way back
```
This shows the pipeline wrapping the controller like an onion — middleware fires going *in* and again coming *out*.

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
> If every action needs its own try-catch and logger calls, you are repeating yourself and violating the **Single Responsibility Principle**. The solution is a **global exception-handling middleware** that catches all exceptions in one place. See [CustomMiddleware.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Middleware/CustomMiddleware.cs) and [TestMiddleware.cs](file:///c:/Users/mmwmn/source/repos/ApiSessions/ApiSessions/Middleware/TestMiddleware.cs) for how custom middleware is structured in this project.

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
7. **Handle Errors** (`ILogger` + try-catch in `TaskItemController.cs`)
8. **Add Middleware** (`CustomMiddleware.cs`, `TestMiddleware.cs` — inline / class-based / `IMiddleware`)
9. **Swap Implementations** (`FakeService` vs `TaskService` via DI)
10. **Understand Lifetimes** (`TransientService`, `ScopedService`, `SingletonService`)
11. *(Next)* **Global Exception Middleware** (catch all unhandled exceptions in one place)
