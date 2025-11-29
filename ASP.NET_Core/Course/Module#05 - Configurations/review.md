# ✅ **Module 05 — Configurations**

ASP.NET Core configuration = flexible system for reading key-value data from **many sources**.

---

# **05_01 — Configuration Overview**

Configuration in ASP.NET Core:

* Stores app settings (strings, numbers, booleans, connection strings, etc.)
* Is **hierarchical** (supports sections)
* Is **combined** from multiple sources
* Uses **Configuration Providers** to load configuration values

Example:

```json
{
  "AppSettings": {
    "Title": "My API",
    "Version": 2
  }
}
```

You access it via:

```csharp
var title = builder.Configuration["AppSettings:Title"];
```

---

# **05_02 — Host vs Application Configuration**

ASP.NET Core has **two layers**:

### **1. Host Configuration**

Used at the **very beginning** before app starts.

Includes:

* Environment (Development/Production)
* Logging settings
* Host URLs

Loaded first.

### **2. Application Configuration**

Used by your actual app.

Includes:

* appsettings.json
* Connection strings
* Swagger settings
* Custom config

Loaded after the Host config.

---

# **05_03 — Configuration Providers**

ASP.NET Core loads configuration from many sources, such as:

✔ Appsettings.json
✔ Appsettings.Environment.json
✔ Environment variables
✔ Command-line arguments
✔ User secrets
✔ In-memory
✔ Azure Key Vault
✔ Custom providers (you can write your own)

Each source = a **Configuration Provider**

---

# **05_04 — System Environment Variables**

Configuration can come from actual OS environment variables.

Example:

```
MyApp__Db__ConnectionString="Server=.;Database=X;Trusted_Connection=True"
```

Note the **double underscore** `__` = used for nested sections.

To read:

```csharp
var conn = builder.Configuration["MyApp:Db:ConnectionString"];
```

Useful in production (Docker, CI/CD, cloud).

---

# **05_05 — Project-specific Environment Variables**

You can set environment variables **for a specific project**:

* Visual Studio → Debug → Environment Variables
* launchSettings.json → "environmentVariables"

Example:

```json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development",
  "API_KEY": "12345"
}
```

These override global OS variables **only for this project**.

---

# **05_06 — Command-line Providers**

You can pass configuration through command-line:

```
dotnet run --Logging:LogLevel:Microsoft=Debug --ApiKey=XYZ
```

Command-line has very **high priority**, it overrides almost everything.

---

# **05_07 — User Secrets (Development only)**

User secrets are stored **outside the project** so they don’t get committed.

Use them for sensitive data like API keys in development.

Enable:

```
dotnet user-secrets init
```

Set:

```
dotnet user-secrets set "Jwt:Key" "abcd1234"
```

Read normally:

```csharp
var key = builder.Configuration["Jwt:Key"];
```

Only works when `ASPNETCORE_ENVIRONMENT = "Development"`.

---

# **05_08 — File-based Providers**

ASP.NET reads from:

* appsettings.json
* appsettings.Development.json
* appsettings.Production.json
* custom JSON files
* INI files
* XML files

Common:

```csharp
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
```

File-based settings = most common configuration source.

---

# **05_09 — In-Memory Configuration**

You can load config from code:

```csharp
builder.Configuration.AddInMemoryCollection(
    new Dictionary<string, string>
    {
        ["Greeting"] = "Hello",
        ["Number"] = "42"
    });
```

Useful for:

* Testing
* Default values
* Feature flags

---

# **05_10 — Order & Override Rules**

Understanding configuration precedence is critical.

Priority: (LOW → HIGH)

1. appsettings.json
2. appsettings.{Environment}.json
3. User Secrets
4. Environment variables
5. Command-line arguments

Higher priority **overrides** lower ones.

Example:

* appsettings.json says `"ApiKey": "123"`
* env variable says `"ApiKey": "999"`

Result = **999**

---

# **05_11 — Accessing Configuration**

You access config through:

```csharp
builder.Configuration["Key"]
builder.Configuration["Section:Key"]
builder.Configuration.GetSection("Section")
```

Example:

```csharp
var section = builder.Configuration.GetSection("JwtSettings");
var key = section["Key"];
```

Tip: Every value comes as **string**, convert manually if needed:

```csharp
int timeout = builder.Configuration.GetValue<int>("CacheTimeout");
```

---

# **05_12 — Options Pattern**

Best and cleanest way to bind configuration into **strongly typed classes**.

Step 1: Create class

```csharp
public class JwtSettings
{
    public string Key { get; set; }
    public int ExpiryMinutes { get; set; }
}
```

Step 2: Register it

```csharp
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));
```

Step 3: Inject it

```csharp
public class AuthService
{
    private readonly JwtSettings _jwt;

    public AuthService(IOptions<JwtSettings> options)
    {
        _jwt = options.Value;
    }
}
```

✔ Clean
✔ Validated
✔ Organized

---

# **05_13 — Configuration Comparison**

Summary of each configuration source:

| Source                       | Use Case                               |
| ---------------------------- | -------------------------------------- |
| appsettings.json             | Default settings                       |
| appsettings.Environment.json | Per-environment settings               |
| User Secrets                 | Sensitive dev settings                 |
| Environment variables        | Production, Docker, cloud              |
| Command-line                 | Temporary or testing overrides         |
| In-memory                    | Testing or defaults                    |
| Azure Key Vault              | Highly sensitive secrets in production |

---
