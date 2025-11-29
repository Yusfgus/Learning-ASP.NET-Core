# ✅ **Module 07 — Middleware**

## **07_01 — Middleware Overview**

Middleware = Code that runs **between the request and the response**.

Think of ASP.NET Core as a **pipeline** of components.
Each middleware can:

1. **Inspect the request**
2. **Modify the request**
3. **Decide to pass it to the next middleware**
4. **Inspect/modify the response** (after the next middleware finishes)

Examples of built-in middleware:

* Authentication middleware
* Authorization middleware
* Routing
* Static Files
* CORS
* Exception handling

---

## **07_02 — Pipeline & Middleware**

The request pipeline is created in **Program.cs** using methods like:

* `Use...`
* `Run...`
* `Map...`

Execution flow:

```
Request → [Middleware A] → [Middleware B] → [Middleware C] → Endpoint
                                          ↑
                                  Response goes back the same way
```

Order **matters a LOT**.

---

## **07_03 — Writing Middleware with Use()**

`app.Use()` lets you write middleware that can:

✔ Run **before** the next middleware
✔ Run **after** the next middleware
✔ You must call `await next()` to continue the pipeline

Example:

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Before");
    await next();
    Console.WriteLine("After");
});
```

---

## **07_04 — Writing Middleware with Run()**

`app.Run()`:

* Terminal middleware
* **Stops the pipeline**
* Does NOT call next()

Example:

```csharp
app.Run(async context =>
{
    await context.Response.WriteAsync("End of pipeline");
});
```

Anything after `Run()` will **not execute**.

---

## **07_05 — Never Call next() After Response**

Once you send a response (e.g., write to the body), you **cannot modify it anymore**.

This is a problem:

```csharp
await context.Response.WriteAsync("Hello");
await next(); // ❌ WRONG
```

Because the response is already committed to the client.

Doing this may cause:

* Exception: "Headers already sent"
* Corrupted response
* Mixed output

General rule:

> **Only call `next()` if you haven’t finished the response yet.**

---

## **07_06 — Before & After Middleware**

This video explains the concept of middleware that executes:

* Code **before** the next middleware
* Code **after** it returns

Example:

```csharp
app.Use(async (context, next) =>
{
    // BEFORE
    Console.WriteLine("Incoming request");

    await next();

    // AFTER
    Console.WriteLine("Outgoing response");
});
```

The "after" part runs while the response is being sent **on the way back**.

---

## **07_07 — Middleware Ordering**

Very important:

> **The order you register middleware defines how your whole API works.**

Common pipeline order:

1. Exception handling
2. Static files
3. Routing
4. Authentication
5. Authorization
6. Endpoints

Example:

```csharp
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

If you mix the order incorrectly, for example:

```csharp
app.MapControllers();
app.UseAuthorization();
```

Authorization will **never run**.

---

## **07_08 — Middleware Branching**

Branching = Creating different pipelines for different paths.

There are 2 main ways:

### **1. Map()**

Branches based on URL prefix:

```csharp
app.Map("/admin", adminApp =>
{
    adminApp.Use(async (context, next) =>
    {
        Console.WriteLine("Admin section");
        await next();
    });
});
```

All requests starting with `/admin` go into this branch.

---

### **2. MapWhen()**

Branch based on **condition**:

```csharp
app.MapWhen(context => context.Request.Query.ContainsKey("debug"), debugApp =>
{
    debugApp.Run(async context =>
    {
        await context.Response.WriteAsync("Debug mode");
    });
});
```

---

### **3. MapGet(), MapPost(), etc.**

For endpoints, not full middleware chains:

```csharp
app.MapGet("/hello", () => "Hello");
```

---

