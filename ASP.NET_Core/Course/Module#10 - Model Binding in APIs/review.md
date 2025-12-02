# 📘 **Module #10 — Model Binding in ASP.NET Core APIs**

Model Binding is the process where ASP.NET Core **automatically reads incoming HTTP request data** and places it into your method parameters.

It can bind from:

* Route parameters
* Query strings
* Headers
* Forms
* Body (JSON/XML)
* Cookies

Below is the module explained *exactly in the same order*.

---

# 🔵 **10.01 — Binding Overview**

Model Binding does this automatically:

### Example:

```csharp
[HttpGet("users/{id}")]
public IActionResult GetUser(int id, string name)
{
    return Ok(new { id, name });
}
```

* `id` comes from the **route**
* `name` comes from the **query string** (e.g., `?name=yousef`)

You don’t need to manually read the request.

---

# 🔵 **10.02 — Route Parameters**

Route parameters come from the URL patterns like:

```
GET /products/5
```

Controller example:

```csharp
[HttpGet("products/{id}")]
public IActionResult GetProduct([FromRoute] int id)
{
    return Ok(new { id });
}
```

* `[FromRoute]` is **optional** because ASP.NET automatically maps `{id}` to the parameter `id`.

### Complex Types From Route

```csharp
[HttpGet("orders/{year}/{month}")]
public IActionResult GetOrders([FromRoute] OrderFilter filter)
{
    return Ok(filter);
}

public class OrderFilter
{
    public int Year { get; set; }
    public int Month { get; set; }
}
```

---

# 🔵 **10.03 — Query Strings**

Query string example:

```
GET /products?category=phones&sort=price
```

API:

```csharp
[HttpGet("products")]
public IActionResult ListProducts([FromQuery] string category, [FromQuery] string sort)
{
    return Ok(new { category, sort });
}
```

### Binding Complex Query Objects

```csharp
public class ProductQuery
{
    public string Category { get; set; }
    public string Sort { get; set; }
    public int Page { get; set; }
}

[HttpGet("products/search")]
public IActionResult SearchProducts([FromQuery] ProductQuery q)
{
    return Ok(q);
}
```

### Binding Arrays / Lists

URL:

```
/items?ids=1&ids=2&ids=3
```

Code:

```csharp
public IActionResult GetItems([FromQuery] List<int> ids)
{
    return Ok(ids); // [1, 2, 3]
}
```

---

# 🔵 **10.04 — Headers**

Binding a single request header:

```csharp
[HttpGet("secure")]
public IActionResult Secure([FromHeader(Name = "X-API-KEY")] string apiKey)
{
    return Ok(apiKey);
}
```

### Binding Multiple Headers into an Object

```csharp
public class ClientInfo
{
    public string Platform { get; set; }
    public string Version { get; set; }
}

[HttpGet("client")]
public IActionResult GetClient([FromHeader] ClientInfo info)
{
    return Ok(info);
}
```

---

# 🔵 **10.05 — Forms**

Used when a POST form is submitted as:

* `application/x-www-form-urlencoded`
* `multipart/form-data` (file uploads)

### Simple form binding:

```csharp
[HttpPost("login")]
public IActionResult Login([FromForm] LoginModel model)
{
    return Ok(model);
}

public class LoginModel 
{
    public string Username { get; set; }
    public string Password { get; set; }
}
```

### File upload example:

```csharp
[HttpPost("upload")]
public IActionResult Upload([FromForm] IFormFile file)
{
    return Ok(new { file.FileName, file.Length });
}
```

Multiple files:

```csharp
public IActionResult UploadMany([FromForm] List<IFormFile> files)
{
    return Ok(new { Count = files.Count });
}
```

---

# 🔵 **10.06 — Body Binding**

Body binding works with:

* JSON
* XML
* Custom content types

Typical example:

### POST JSON Request

```json
{
  "name": "Laptop",
  "price": 2000
}
```

Controller:

```csharp
[HttpPost("products")]
public IActionResult CreateProduct([FromBody] Product model)
{
    return Ok(model);
}
```

### Complex type required

You cannot bind **simple types** from body unless `[FromBody]` is explicitly used.

---

# 🔵 **10.07 — Cookies**

Binding from request cookies:

```csharp
[HttpGet("profile")]
public IActionResult Profile([FromCookie(Name = "session-id")] string sessionId)
{
    return Ok(sessionId);
}
```

You can also bind to a class:

```csharp
public class UserCookies
{
    public string Theme { get; set; }
    public string SessionId { get; set; }
}

[HttpGet("settings")]
public IActionResult Settings([FromCookie] UserCookies cookies)
{
    return Ok(cookies);
}
```

### Writing / Reading Cookies:

```csharp
Response.Cookies.Append("session-id", "xyz123");
var cookie = Request.Cookies["session-id"];
```

---

# ✔ Summary — Where Data Comes From?

| Source       | Attribute      | Example             |
| ------------ | -------------- | ------------------- |
| Route        | `[FromRoute]`  | `/users/5`          |
| Query String | `[FromQuery]`  | `?page=2&sort=name` |
| Header       | `[FromHeader]` | `X-API-KEY: abc123` |
| Form         | `[FromForm]`   | HTML form POST      |
| Body         | `[FromBody]`   | JSON payload        |
| Cookie       | `[FromCookie]` | browser cookies     |

---
