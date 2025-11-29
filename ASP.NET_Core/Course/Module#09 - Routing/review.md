# ✅ **Module 09 — Routing**

Routing is the system that decides **which endpoint** should handle an incoming request, and how URL parameters are mapped.

---

# **09_01 — Routing Overview**

Routing:

* Matches **incoming HTTP requests** → **endpoints**
* Generates URLs (Link Generation)
* Works for **Controllers**, **Minimal APIs**, **Razor Pages**, **SignalR**, etc.

Routing = “How ASP.NET Core maps URLs → code”

---

# **09_02 — What Is Routing?**

Routing is made of two main concepts:

### **1. Route Matching**

Incoming HTTP request:

```
GET /products/5
```

Matched to:

```csharp
app.MapGet("/products/{id}", ...);
```

### **2. Route Binding**

Route values are bound automatically to parameters in the handler:

```csharp
app.MapGet("/products/{id}", (int id) => ... );
```

Routing also supports:

* Query parameters
* Optional parameters
* Catch-all parameters
* Constraints
* Default values

Routing sits in the **Middleware Pipeline**.

---

# **09_03 — Request Matching & Execution**

ASP.NET Core routing uses:

### **Middleware 1 — UseRouting**

This matches the endpoint.

### **Middleware 2 — UseEndpoints / MapX**

This executes the endpoint.

The flow:

```
HTTP Request → UseRouting (match endpoint) → Authorization → UseEndpoints (execute endpoint)
```

Important:

✔ Matching and execution are separate
✔ Some middlewares must be placed between them (auth, CORS)

---

# **09_04 — UseRouting Order Matters**

Order in middleware decides behavior.

Correct order:

```csharp
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
```

Wrong order → routing fails

Example of mistake:

```csharp
app.UseAuthorization(); // ❌ before UseRouting
app.UseRouting();
```

Routing must be placed early to allow other middleware to use route data.

---

# **09_05 — Route Templates**

Route templates define how URLs map to endpoints.

### Basic template

```csharp
app.MapGet("/products/{id}", ...);
```

### Optional parameter

```csharp
"/products/{id?}"
```

### Default value

```csharp
"/products/{id=1}"
```

### Multiple parameters

```csharp
"/customers/{customerId}/orders/{orderId}"
```

### Catch-all (wildcard)

```csharp
"/files/{**path}"
```

Matches:

```
/files/a/b/c/d.txt
```

### Constraints (explained later)

```
"/products/{id:int}"
```

### Mixed segments

```
"/blog/{year:int}/{title}"
```

Route templates support:

* Literal segments: `blog`, `users`
* Parameter segments: `{id}`
* Optional parameters: `{id?}`
* Wildcard: `*` or `**`
* Defaults
* Constraints

---

# **09_06 — Route Constraints**

Used to restrict route matching.

Some built-in constraints:

| Constraint | Example                    | Meaning          |
| ---------- | -------------------------- | ---------------- |
| int        | `{id:int}`                 | only integers    |
| guid       | `{id:guid}`                | only valid GUIDs |
| bool       | `{flag:bool}`              | true/false       |
| min/max    | `{age:min(18)}`            | value ≥ 18       |
| length     | `{name:length(3,10)}`      | 3–10 chars       |
| regex      | `{code:regex(^[A-Z]{3}$)}` | must match regex |

Example:

```csharp
app.MapGet("/orders/{id:int}", ...);
```

If user calls:

```
/orders/abc
```

→ **404** (does not match constraint)

You can also combine constraints:

```
"/blog/{year:int:min(2000)}/{month:int:range(1,12)}"
```

Custom constraints can be registered too (but rarely needed).

---

# **09_07 — Link Generators**

Routing is **two-way**:

1. Match incoming request → endpoint
2. Generate URLs → from endpoints

Examples of link generation:

### Using LinkGenerator

```csharp
public class MyService
{
    private readonly LinkGenerator _links;

    public MyService(LinkGenerator links)
    {
        _links = links;
    }

    public string Generate()
    {
        return _links.GetPathByName("GetProduct", new { id = 10 });
    }
}
```

### Using Named Routes

```csharp
app.MapGet("/products/{id}", ...)
   .WithName("GetProduct");
```

Now you can generate URLs without hardcoding route strings.

This is important when:

* Routes change
* You want cleaner architecture
* You build email links
* You redirect internally

---

# **09_08 — Parameter Transformers**

Transformers automatically change parameter values in route URLs.

Example: **SlugifyTransformer**

Register transformer:

```csharp
builder.Services.AddRouting(options =>
{
    options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
});
```

Example transformer:

```csharp
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object? value)
    {
        return value?.ToString()?.ToLower().Replace(" ", "-") ?? "";
    }
}
```

Use in routes:

```csharp
[Route("products/[controller:slugify]/[action:slugify]")]
public class MyAwesomeController : Controller
{
}
```

Result:

Class: MyAwesomeController → `my-awesome`
Action: GetProductDetails → `get-product-details`

Parameter transformers auto-format:

* Controllers
* Actions
* Route segments

They make your URLs nicer, consistent, SEO-friendly.

---

# 🎯 **Module 09 — Summary**

Routing in ASP.NET Core:

### ✔ Matches requests

### ✔ Executes endpoints

### ✔ Generates URLs

### ✔ Supports constraints

### ✔ Supports parameter transformers

### ✔ Middleware order is important

### ✔ Works with minimal APIs, controllers, Razor Pages

---
