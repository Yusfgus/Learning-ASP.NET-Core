A **CancellationToken** is a **cooperative stop signal** used in .NET to **cancel ongoing work safely**, especially **async operations**.

Think of it as:

> “Someone may ask this operation to stop — be ready to stop cleanly.”

---

## Why CancellationToken Exists

Without cancellation:

* Async operations keep running even if:

  * HTTP request is aborted
  * User navigates away
  * Timeout happens
* Wasted CPU, memory, DB connections

CancellationToken allows:

* Early exit
* Resource cleanup
* Better scalability

---

## Basic Idea (Mental Model)

```
Caller  ──▶  Operation (with token)
         ◀──  Cancel request
```

The operation **chooses** to stop when cancellation is requested.

---

## Core Types

### 1️⃣ CancellationToken

* Read-only
* Passed to methods

```csharp
CancellationToken token
```

### 2️⃣ CancellationTokenSource

* Issues the cancellation signal

```csharp
var cts = new CancellationTokenSource();
cts.Cancel();
```

---

## Simple Example

```csharp
var cts = new CancellationTokenSource();

Task.Run(() =>
{
    while (!cts.Token.IsCancellationRequested)
    {
        Console.WriteLine("Working...");
        Thread.Sleep(500);
    }
});

Thread.Sleep(2000);
cts.Cancel();
```

---

## Cancellation in ASP.NET Core (VERY IMPORTANT)

ASP.NET Core automatically provides a token:

```csharp
HttpContext.RequestAborted
```

This token is cancelled when:

* Client disconnects
* Request times out
* Server shuts down

---

## Using CancellationToken in Controller

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(
    int id,
    CancellationToken cancellationToken)
{
    var product = await _dbContext.Products
        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    return product == null ? NotFound() : Ok(product);
}
```

✔ No extra code needed
✔ Automatically wired by ASP.NET Core

---

## CancellationToken with EF Core

Most EF Core async methods support it:

```csharp
await _dbContext.SaveChangesAsync(cancellationToken);
```

If the request is cancelled → DB query stops.

---

## CancellationToken with HttpClient

```csharp
await httpClient.GetAsync(url, cancellationToken);
```

Prevents:

* Hanging requests
* Resource leaks

---

## Throwing on Cancellation (Optional)

```csharp
token.ThrowIfCancellationRequested();
```

Throws `OperationCanceledException`.

Used when:

* You want immediate exit
* Cleanup is handled upstream

---

## Linking Tokens (Advanced)

```csharp
using var cts = CancellationTokenSource.CreateLinkedTokenSource(
    HttpContext.RequestAborted,
    timeoutToken);
```

Cancels when **either** token cancels.

---

## CancellationToken ≠ Kill Thread ❌

Important:

* Does NOT force-stop code
* Does NOT kill threads
* Requires cooperation

Bad code ignoring token will keep running.

---

## Common Mistakes

❌ Ignoring the token
❌ Creating new tokens unnecessarily
❌ Catching `OperationCanceledException` and swallowing it

---

## One-Sentence Summary

> **CancellationToken is a cooperative signal that allows async operations to stop early when work is no longer needed.**

---

