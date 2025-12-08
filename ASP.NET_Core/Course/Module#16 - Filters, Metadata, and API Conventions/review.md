# ⭐ 16_01 — Introduction to Filters

**Filters = middleware but only at the MVC level**

They let you run code:

* **Before** a controller action
* **After** a controller action
* **After result formatting**
* **On exceptions**

Used for:

✔ Logging
✔ Validation
✔ Caching
✔ Authentication / Security
✔ Transforming results
✔ Error handling

---

---

# ⭐ 16_02 — Filter Types in ASP.NET Core

There are 5 main filter interfaces:

| Type                           | Interface          | Runs                                         |
| ------------------------------ | ------------------ | -------------------------------------------- |
| Action Filter                  | `IActionFilter`    | Before and after controller action           |
| Resource Filter                | `IResourceFilter`  | Very early, around routing and model binding |
| Result Filter                  | `IResultFilter`    | Before and after result execution            |
| Exception Filter               | `IExceptionFilter` | When exceptions happen                       |
| Endpoint Filter (Minimal APIs) | `IEndpointFilter`  | For Minimal API endpoints                    |

---

---

# ⭐ 16_03 — Filter Execution Pipeline

Order of execution:

```
Resource Filters
  Action Filters
    Controller Action
  Action Filters
Resource Filters
Result Filters
Exception Filters (if exception happens)
```

Think:

* **Resource filter** = around whole request/response
* **Action filter** = around controller action only
* **Result filter** = around returning response only
* **Exception filter** = only when errors happen

---

---

# 🧨 From here: FULL WORKING CODE

Assume a normal `.NET 8 WebAPI Controller` project.

`Program.cs`:

```csharp
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();
```

---

---

# ⭐ 16_04 — Implementing Action Filters

### 🔥 Purpose

Run logic **before and after** an action executes.

### 📌 Create a custom action filter

```csharp
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

public class TimingActionFilter : IActionFilter
{
    private Stopwatch _watch;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _watch = Stopwatch.StartNew();
        Console.WriteLine("Action started...");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _watch.Stop();
        Console.WriteLine($"Action completed in {_watch.ElapsedMilliseconds} ms");
    }
}
```

---

### 📌 Apply to a controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    [ServiceFilter(typeof(TimingActionFilter))]
    public IActionResult Get() => Ok("Hello");
}
```

---

### 📌 Register filter in DI

```csharp
builder.Services.AddScoped<TimingActionFilter>();
```

---

### ✔ Output

```
Action started...
Action completed in 3 ms
```

---

---

# ⭐ 16_05 — Implementing Resource Filters

### 🔥 Purpose

* Runs **before model binding & action**
* Good for **caching**, **short-circuiting requests**

### 📌 Code

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CacheResourceFilter : IResourceFilter
{
    private static string? _cachedResult;

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        if (_cachedResult != null)
        {
            context.Result = new ContentResult
            {
                Content = _cachedResult,
                ContentType = "text/plain"
            };
        }
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        if (_cachedResult == null && context.Result is ContentResult result)
        {
            _cachedResult = result.Content;
        }
    }
}
```

---

### 📌 Apply to controller

```csharp
[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(CacheResourceFilter))]
public class TimeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(DateTime.Now.ToString());
}
```

---

### ✔ Behavior

* First request: returns real time
* Second request: cached result (no action executed)

---

---

# ⭐ 16_06 — Implementing Result Filters

### 🔥 Purpose

Modify the **response** right before sending to client.

### 📌 Example: wrap responses

```csharp
using Microsoft.AspNetCore.Mvc.Filters;

public class WrapResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("Before writing result");
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("After writing result");
    }
}
```

### 📌 Apply

```csharp
[ServiceFilter(typeof(WrapResultFilter))]
[HttpGet]
public IActionResult GetUser()
{
    return Ok(new { Name = "Gus", Role = "Dev" });
}
```

---

---

# ⭐ 16_07 — Implementing Exception Filters

### 🔥 Purpose

Catch unhandled exceptions in controller actions and return custom response.

### 📌 Code

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new
        {
            message = context.Exception.Message,
            exception = context.Exception.GetType().Name
        })
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}
```

---

### 📌 Register globally (best practice)

```csharp
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
});
```

---

### 📌 Controller test

```csharp
[HttpGet("fail")]
public IActionResult Fail()
{
    throw new Exception("Something went wrong");
}
```

### ✔ Response

```json
{
  "message": "Something went wrong",
  "exception": "Exception"
}
```

*No crash, no 500 white page.*

---

---

# ⭐ 16_08 — Filters in Minimal APIs (NEW in .NET 7+)

Minimal APIs use **IEndpointFilter**

---

### 📌 Create filter

```csharp
public class LoggingEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        Console.WriteLine("Before endpoint");

        var result = await next(context);

        Console.WriteLine("After endpoint");

        return result;
    }
}
```

---

### 📌 Apply to minimal API

```csharp
var app = WebApplication.CreateBuilder(args)
    .Build();

app.MapGet("/hello", () => "Hello minimal")
   .AddEndpointFilter(new LoggingEndpointFilter());

app.Run();
```

---

### ✔ Output

```
Before endpoint
After endpoint
```

---

---

# ⭐ 16_09 — Filter Best Practices

✔ Use **Exception Filters** instead of try/catch everywhere
✔ Use **Action Filters** for audit logs & validation
✔ Use **Resource Filters** for caching
✔ Register **global filters** to apply to all controllers
✔ Avoid business logic in filters

---

---

# 🌟 Bonus — Register Global Filter (recommended)

```csharp
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<LoggingActionFilter>();
});
```

---

---

# 🧠 Summary

| Filter                    | Typical Use                                 |
| ------------------------- | ------------------------------------------- |
| Action Filter             | Logging, validation                         |
| Resource Filter           | Caching, request short-circuiting           |
| Result Filter             | Modify outgoing response                    |
| Exception Filter          | Global error handling                       |
| Endpoint Filter (Minimal) | Same as Action filters but for minimal APIs |
