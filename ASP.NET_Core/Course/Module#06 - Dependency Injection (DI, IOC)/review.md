# ✅ **Module 06 — Dependency Injection (DI, IoC)**

ASP.NET Core is built 100% around **Dependency Injection**, so mastering this module is essential.

I’ll go lesson by lesson in a clear way.

---

# **06_01 — DI Overview**

**Dependency Injection (DI)** = a technique where classes **do not create their own dependencies**.

Instead, dependencies are **injected** into them.

Example (bad, without DI):

```csharp
public class OrderService
{
    private readonly EmailService _email = new EmailService();
}
```

Example (good, with DI):

```csharp
public class OrderService
{
    private readonly IEmailService _email;

    public OrderService(IEmailService email)
    {
        _email = email;
    }
}
```

Why DI?

* Reduces coupling
* Easier testing (use mocks)
* Follows SOLID (especially DIP)
* Flexible and extensible

---

# **06_02 — Tight Coupling**

Tightly coupled code is code where:

1. Classes control everything by themselves
2. They create their own dependencies
3. Changing one class requires changing others
4. Hard to test
5. Hard to replace implementations

Example of tight coupling:

```csharp
var repo = new SqlRepository();
var service = new ProductService(repo);
```

If you change SqlRepository → MongoRepository, you must edit code everywhere.

DI helps solve this.

---

# **06_03 — Dependency Inversion Principle (DIP)**

Part of the SOLID principles.

DIP says:

> **High-level modules should not depend on low-level modules. Both should depend on abstractions.**
> **Abstractions should not depend on details. Details should depend on abstractions.**

Meaning:

* Use interfaces
* Depend on “what it does,” not “how it does it”

Bad:

```csharp
public class PaymentService
{
    private readonly PayPalPayment _paypal;
}
```

Good:

```csharp
public class PaymentService
{
    private readonly IPaymentGateway _paymentGateway;
}
```

---

# **06_04 — IoC Containers**

IoC Container = Dependency Injection Container.

Responsibilities:

* Register services
* Resolve services
* Manage lifetimes (Scoped, Singleton, Transient)
* Build object graphs

In ASP.NET Core:

```csharp
builder.Services.AddTransient<IEmailService, EmailService>();
```

The IoC container creates `EmailService` automatically when needed.

---

# **06_05 — Constructor Injection by Default**

ASP.NET Core uses **Constructor Injection** as the primary method.

Example:

```csharp
public class HomeController : Controller
{
    private readonly IProductService _service;

    public HomeController(IProductService service)
    {
        _service = service;
    }
}
```

Controller, middleware, filters, everything supports constructor injection.

Property injection and method injection are possible but not recommended.

---

# **06_06 — Built-in ASP.NET Core Services**

ASP.NET Core registers many services automatically:

* Logging → `ILogger<T>`
* Configuration → `IConfiguration`
* Hosting → `IHostEnvironment`
* HttpContext → `IHttpContextAccessor`
* Routing → endpoint routing
* Authentication/Authorization
* Options pattern

Example:

```csharp
public class MyService
{
    public MyService(ILogger<MyService> logger)
    {
        logger.LogInformation("Hello");
    }
}
```

You can inject built-in services exactly like your own.

---

# **06_07 — Grouping Services**

You can group registration logic into extension methods.

Instead of this in Program.cs:

```csharp
builder.Services.AddTransient<A>();
builder.Services.AddTransient<B>();
builder.Services.AddTransient<C>();
```

Create a static extension:

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyBusinessServices(this IServiceCollection services)
    {
        services.AddTransient<A>();
        services.AddTransient<B>();
        services.AddTransient<C>();
        return services;
    }
}
```

Then call:

```csharp
builder.Services.AddMyBusinessServices();
```

✔ Cleaner
✔ Organized
✔ Modular

---

# **06_08 — Registration Extensions vs Descriptors**

### **Extensions (AddTransient, AddSingleton, etc.)**

Easy to use:

```csharp
services.AddTransient<IRepo, SqlRepo>();
```

### **Service Descriptors**

More low-level, more control:

```csharp
services.Add(new ServiceDescriptor(
    typeof(IRepo),
    typeof(SqlRepo),
    ServiceLifetime.Transient
));
```

Usually you should use extension methods unless you need full control.

---

# **06_09 — Multiple Registrations**

You can register **multiple implementations** of one interface.

```csharp
services.AddTransient<INotifier, EmailNotifier>();
services.AddTransient<INotifier, SmsNotifier>();
```

Then inject **IEnumerable<INotifier>**:

```csharp
public class AlertService
{
    public AlertService(IEnumerable<INotifier> notifiers)
    {
        foreach (var notifier in notifiers)
            notifier.Notify("Hello");
    }
}
```

Order of registration = order of enumeration.

---

# **06_10 — Keyed Services**

You can register services with **keys** (ASP.NET Core 8+).

```csharp
services.AddKeyedTransient<IPayment>("paypal", sp => new PayPal());
services.AddKeyedTransient<IPayment>("visa", sp => new Visa());
```

Injection:

```csharp
public class CheckoutService
{
    public CheckoutService([FromKeyedServices("paypal")] IPayment payment)
    {
        // ...
    }
}
```

Useful for:

* Strategies
* Payment gateways
* Providers

---

# **06_11 — Factory Instantiation**

You can create services using factories:

```csharp
services.AddTransient<IMailer>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new Mailer(config["Mail:Host"]);
});
```

Use when:

* Constructor parameters are dynamic
* You need logic for choosing implementation
* Something depends on runtime values

---

# **06_12 — Resolving at Startup**

If you need to resolve something at startup:

```csharp
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetRequiredService<IMyService>();
service.RunStartupTask();
```

Never resolve scoped services from root provider.

Always create a scope.

---

# **06_13 — Service Lifetimes**

The most important part.

### **Transient**

New instance **every time** it’s requested.

```csharp
services.AddTransient<IMyService, MyService>();
```

Use for:

* Lightweight, stateless logic

---

### **Scoped**

One instance per **HTTP request**.

```csharp
services.AddScoped<IMyService, MyService>();
```

Use for:

* Repositories
* Unit of Work
* DbContext

MOST COMMON in Web APIs.

---

### **Singleton**

One instance **for the whole application lifetime**.

```csharp
services.AddSingleton<IMyService, MyService>();
```

Use for:

* Caches
* Settings
* Background services

Never inject scoped services into a singleton.

---

# 🎉 **Module 06 Summary**

DI in ASP.NET Core gives you:

* Cleaner architecture
* Separation of concerns
* Testability
* Configuration flexibility
* Lifetime management
* Multiple implementations
* Factory and keyed services

---
