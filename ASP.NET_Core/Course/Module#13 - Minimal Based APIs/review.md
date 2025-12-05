# ✅ 13_01 — Minimal API Overview (with code)

Minimal APIs allow you to define endpoints **without controllers or attributes**, directly in `Program.cs`.

### ✔ Example — Basic Minimal API

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello from Minimal API!");

app.Run();
```

### 🧠 Key Concepts

* No controller classes
* No `[HttpGet]` attributes
* Routes are registered on the `app` object

---

---

# ✅ 13_02 — HTTP Verb Support (with code)

Minimal APIs support all common HTTP verbs using **MapXXX** extensions.

---

### ✔ GET

```csharp
app.MapGet("/products", () => "Returning all products");
```

### ✔ POST

```csharp
app.MapPost("/products", (Product p) =>
{
    return $"Product {p.Name} created!";
});
```

### ✔ PUT

```csharp
app.MapPut("/products/{id}", (int id, Product updated) =>
{
    return $"Product {id} updated to {updated.Name}";
});
```

### ✔ PATCH

```csharp
app.MapPatch("/products/{id}", (int id, Product patch) =>
{
    return $"Product {id} partially updated";
});
```

### ✔ DELETE

```csharp
app.MapDelete("/products/{id}", (int id) =>
{
    return $"Product {id} deleted";
});
```

---

---

# ✅ 13_03 — Endpoint Anatomy (with code)

An endpoint in Minimal APIs contains:

1. Route pattern
2. Delegate (function)
3. Dependency injection parameters
4. Result returned

---

### ✔ Example using Dependency Injection + Route Parameters

```csharp
app.MapGet("/orders/{id}", (int id, AppDbContext db) =>
{
    var order = db.Orders.Find(id);
    return order is null ? Results.NotFound() : Results.Ok(order);
});
```

### 🔥 Explanation

* `int id` comes from the **route**
* `AppDbContext db` is resolved using **DI**
* We return **typed results** from `Results` static class

---

---

# ✅ 13_04 — Response Generation (with code)

Minimal APIs use the **Results** class to generate HTTP responses.

---

### ✔ Returning JSON

```csharp
app.MapGet("/profile", () =>
{
    return Results.Json(new { Name = "Yousef", Country = "Egypt" });
});
```

### ✔ Returning custom Status Codes

```csharp
app.MapGet("/error", () =>
{
    return Results.StatusCode(500);
});
```

### ✔ Returning Created (201)

```csharp
app.MapPost("/users", (User user) =>
{
    // save user...
    return Results.Created($"/users/{user.Id}", user);
});
```

### ✔ Returning NotFound

```csharp
app.MapGet("/users/{id}", (int id) =>
{
    return id == 10 ? Results.Ok("Found") : Results.NotFound();
});
```

---

---

# ✅ 13_05 — Grouped Endpoints (with code)

This organizes routes by **prefix** or **version**.

---

### ✔ Example — Group by resource

```csharp
var products = app.MapGroup("/products");

products.MapGet("/", () => "Get all products");
products.MapGet("/{id}", (int id) => $"Get product {id}");
products.MapPost("/", (Product p) => $"Add {p.Name}");
products.MapDelete("/{id}", (int id) => $"Delete product {id}");
```

### 🔥 Benefits

* Cleaner API structure
* Perfect for large systems

---

### ✔ Example — Versioning groups

```csharp
var v1 = app.MapGroup("/api/v1");
var v2 = app.MapGroup("/api/v2");

v1.MapGet("/data", () => "V1 data");
v2.MapGet("/data", () => "V2 data");
```

---

---

# ✅ 13_06 — Building a Minimal REST API (with code)

Let’s build a real CRUD REST API using **Entity Framework Core InMemory**.

---

## ✔ Step 1 — Add EF Core In-Memory (NuGet)

```
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

---

## ✔ Step 2 — Database + Model

```csharp
public record Product(int Id, string Name);

public class AppDb : DbContext
{
    public AppDb(DbContextOptions options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}
```

---

## ✔ Step 3 — Program.cs full example

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDb>(opt =>
    opt.UseInMemoryDatabase("ProductsDb"));

var app = builder.Build();

var products = app.MapGroup("/products");

// GET ALL
products.MapGet("/", async (AppDb db) =>
    await db.Products.ToListAsync());

// GET BY ID
products.MapGet("/{id}", async (int id, AppDb db) =>
    await db.Products.FindAsync(id) is Product p ? Results.Ok(p) : Results.NotFound());

// POST
products.MapPost("/", async (Product p, AppDb db) =>
{
    db.Products.Add(p);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{p.Id}", p);
});

// PUT
products.MapPut("/{id}", async (int id, Product newProduct, AppDb db) =>
{
    var existing = await db.Products.FindAsync(id);
    if (existing is null) return Results.NotFound();

    existing.Name = newProduct.Name;
    await db.SaveChangesAsync();
    return Results.Ok(existing);
});

// DELETE
products.MapDelete("/{id}", async (int id, AppDb db) =>
{
    var p = await db.Products.FindAsync(id);
    if (p is null) return Results.NotFound();

    db.Products.Remove(p);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
```

---

# 🧠 Summary

| Feature       | Minimal API Code                                          |
| ------------- | --------------------------------------------------------- |
| HTTP verbs    | `MapGet`, `MapPost`, `MapPut`, `MapPatch`, `MapDelete`    |
| Responses     | `Results.Ok()`, `Results.NotFound()`, `Results.Created()` |
| Route Params  | `(int id)`                                                |
| DI Params     | `(AppDb db)`                                              |
| Grouping      | `app.MapGroup("/products")`                               |
| Real REST API | Full CRUD example above                                   |

---

