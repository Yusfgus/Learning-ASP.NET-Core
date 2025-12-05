# 🟦 1. Controller Basics

A **controller** is a class that:

* Lives in the `Controllers` folder
* Must inherit from `ControllerBase` or `Controller`
* Has action methods to handle requests

Example:

```csharp
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
}
```

✔ `ApiController` attribute = automatic validation + binding rules
✔ `Route("api/[controller]")` = class-level route

---

# 🟦 2. Action Results

ASP.NET Core actions **return HTTP responses**, not just data.

Examples:

```csharp
return Ok(data);           // 200
return NotFound();         // 404
return BadRequest(msg);    // 400
return Created(uri, obj);  // 201
return NoContent();        // 204
return StatusCode(500);    // 500
```

Key concept:

> Always return an **IActionResult** or **ActionResult<T>**

---

# 🟦 3. Routing & Parameters

You define routes with tokens, like:

```csharp
[HttpGet("{id:int}")]
public IActionResult GetBook(int id)
{
}
```

✔ `id:int` = route constraint
(Better reliability and faster matching)

---

# 🟦 4. Designing Requests

### For GET /things

Use:

* Route parameters
* Query strings

### For mutation (POST, PUT, PATCH, DELETE)

Always use the **BODY** in JSON:

```csharp
public IActionResult Create([FromBody] BookDto model)
```

---

# 🟦 5. OPTIONS and HEAD

These are **REST completeness endpoints**:

### HEAD

Same as GET but without body
Used for caching or checking if resource exists.

```csharp
[HttpHead("{id}")]
public IActionResult Head(int id) => Ok();
```

### OPTIONS

Tell client what methods are allowed

```csharp
[HttpOptions]
public IActionResult Options()
{
    Response.Headers.Add("Allow", "GET,POST,PUT,DELETE");
    return Ok();
}
```

---

# 🟦 6. GET Endpoints

### GET by ID

```
GET /api/books/10
```

### GET all

```
GET /api/books
```

Use pagination or filtering when large.

---

# 🟦 7. POST (Create)

Return **201 Created** + `Location` header.

```csharp
return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
```

That is **correct REST**.

---

# 🟦 8. PUT vs PATCH

### PUT (replace entire resource)

Client must send **all fields**.

```
PUT /api/books/5
```

### PATCH (partial update)

Send only modified fields.

```
PATCH /api/books/5
Content-Type: application/json-patch+json
```

Example:

```json
[
  { "op": "replace", "path": "/title", "value": "New title" }
]
```

> PATCH is advanced and extremely important in real-world APIs.

---

# 🟦 9. DELETE

Return **204 NoContent** if delete successful.

```csharp
return NoContent();
```

Do not return object on delete.

---

# 🟦 10. Returning 202 Accepted

When the operation is **async or long running**:

```
POST /api/report
202 Accepted
Location: /api/report/status/123
```

Used for:

* Background jobs
* Emails
* File processing
* Long-running tasks

---

# 🟦 11. File Requests

### In-memory file

```csharp
return File(bytes, "application/pdf", "file.pdf");
```

### Physical file

```csharp
return PhysicalFile(path, "image/jpeg");
```

This is super useful for:

* Downloads
* PDFs
* Images

---

# 🟦 12. Redirects

### Temporary (302)

```csharp
return Redirect("https://google.com");
```

### Permanent (301)

```csharp
return RedirectPermanent("/api/new-route");
```

Used for **API versioning** or deprecating endpoints.

---

# 🟦 13. Content Negotiation

This is huge.

Same method returns **different formats** based on `Accept` header:

### Client:

```
Accept: application/json
```

Server returns JSON.

### Client:

```
Accept: application/xml
```

Server returns XML (if formatter registered).

---

# 🔥 Example Content Negotiation Setup

```csharp
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();
```

Now API supports:

* JSON
* XML

Automatically.

---

# 🟦 14. The most important takeaway

> A controller endpoint is not just returning data — it is designing **HTTP responses correctly** using status codes, headers, and body formats.

Once you truly understand these:

✔ You can build proper REST APIs
✔ You can pass technical interviews
✔ You can work with front-end teams easily
✔ You can design endpoints like a pro

---

