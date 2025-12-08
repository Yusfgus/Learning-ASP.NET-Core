# ⭐ Quick Summary

| Feature                                               | Resource Filter          | Action Filter      |
| ----------------------------------------------------- | ------------------------ | ------------------ |
| Runs **before model binding**                         | ✅ YES                    | ❌ NO               |
| Can **short-circuit request early** (skip controller) | ✅ YES                    | ⚠️ Not recommended |
| Useful for **caching whole responses**                | ✅ BEST                   | ❌ BAD              |
| Sees **raw HTTP context**                             | ✅ YES                    | ⚠️ Limited         |
| Runs **before and after action**                      | ⚠️ Yes, but higher level | ✅ YES              |
| Good for **logging inside action only**               | ❌ NOT IDEAL              | ✅ BEST             |
| Can access **model state** and action arguments       | ❌ NO (too early)         | ✅ YES              |

---

---

# ⭐ The Core Difference (Very Important)

## 🔥 RESOURCE filters run **before model binding and before action selection**.

Meaning:

* The controller action has **not executed yet**
* The action parameters **are not bound yet**
* ModelState **does not exist yet**

Resource Filters are closest to middleware.

---

## 🔥 ACTION filters run **after model binding but before the action executes**.

Meaning:

* You have access to:

  * Bound parameters
  * ModelState
  * Action arguments
  * Controller instance

Action Filters are closest to a **wrapper around the action itself**.

---

---

# ⭐ What You Can Do in a ResourceFilter (That ActionFilter Cannot)

### 🟢 1. Check cache and **skip the action entirely**

```csharp
public void OnResourceExecuting(ResourceExecutingContext context)
{
    if (Cache.TryGetValue("users", out var result))
    {
        context.Result = new JsonResult(result); // <-- no action runs
    }
}
```

➡️ Action filter **cannot safely skip** controller execution with caching logic.

---

### 🟢 2. Modify request **before model binding**

Example: change culture before model binder parses dates

```csharp
CultureInfo.CurrentCulture = new("ar-EG");
```

➡️ Action filter is **too late** — model is already bound!

---

### 🟢 3. Implement **global response caching**

Resource filters run **only once per request**, perfect for:

* Caching HTML
* Caching JSON APIs
* Returning static result

---

---

# ⭐ What You Can Do in an ActionFilter (That ResourceFilter Cannot)

### 🔵 1. Access action parameters

```csharp
public void OnActionExecuting(ActionExecutingContext context)
{
    var id = (int)context.ActionArguments["id"];
}
```

➡️ Resource filters cannot do this because **binding hasn't happened**.

---

### 🔵 2. Validate model state

```csharp
if (!context.ModelState.IsValid)
{
    context.Result = new BadRequestObjectResult(context.ModelState);
}
```

➡️ Resource filter has **no ModelState** yet.

---

### 🔵 3. Modify action return value

```csharp
public void OnActionExecuted(ActionExecutedContext context)
{
    var original = context.Result as ObjectResult;
    // alter JSON payload
}
```

➡️ Resource filter sees only raw context / result, not action data.

---

---

# ⭐ Typical Use Cases

## Resource Filters — "High Level / Request Level"

✔ Response caching
✔ Early request rejection
✔ Perf monitoring for whole pipeline
✔ Add headers before binding
✔ API throttling
✔ Disable expensive operations per route

### Think:

**"Do I even need to run the controller?"**

---

## Action Filters — "Action Wrapper"

✔ Logging per action call
✔ Validate data
✔ Modify data before returning JSON
✔ Enrich response
✔ Authentication checks
✔ Audit database actions

### Think:

**"I want to intercept before/after action execution."**

---

---

# ⭐ Quick Mental Model

---

### Resource Filter:

🧠 **I may skip the controller completely. I operate outside MVC world.**
🔧 Closer to middleware
🚫 Cannot touch model binding / action params

---

### Action Filter:

🧠 **I wrap around the controller action execution.**
🔧 Model binding is DONE
✔ Can touch ModelState, action args

---

---

# ⭐ When to Choose Which

---

### Use **ResourceFilter** if:

* You want to check cache and return early
* You want to add **output caching**
* You want to block request before doing work
* You need to measure total request duration
* You need logic similar to middleware but only for MVC

---

### Use **ActionFilter** if:

* You need access to method input parameters
* You want to modify returned data
* You want validation logic
* You want logging per action call
* You want per-controller cross-cutting concerns

---

---

# ⭐ Final Code Comparison

### Resource Filter (Early short-circuit)

```csharp
public void OnResourceExecuting(ResourceExecutingContext context)
{
    if (Cached)
    {
        context.Result = new ContentResult { Content = "Cached value" };
        return;
    }
}
```

Controller never runs.

---

### Action Filter (Validation)

```csharp
public void OnActionExecuting(ActionExecutingContext context)
{
    if (!context.ModelState.IsValid)
    {
        context.Result = new BadRequestObjectResult(context.ModelState);
        return;
    }
}
```

Controller runs **only if** valid.

---

---

# ✔ Final Answer in One Sentence

> **ResourceFilters** run earlier (before model binding) and can short-circuit the whole request, ideal for caching and pre-processing.
> **ActionFilters** run around the controller method after model binding, ideal for validation, logging, and modifying results.
