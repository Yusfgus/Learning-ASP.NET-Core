
using MinimalRFC9457.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Your API will return error responses in RFC 9457 format.
// Exceptions and HTTP errors (404, 400, 500, etc.) will be formatted in a standardized JSON structure.
// ASP.NET Core automatically fills fields like status, title, and optionally type.
builder.Services.AddProblemDetails();

var app = builder.Build();

// In combination with AddProblemDetails(), any exception that happens will:
// - NOT crash the API
// - NOT return plain text
// - Return an RFC 9457-formatted Problem Details JSON response
// Without AddProblemDetails(), ASP.NET would output a plain 500 page instead.
app.UseExceptionHandler();

// This middleware handles HTTP status errors that are not exceptions
// such as: 400 Bad Request, 404 Not Found, 401 Unauthorized, 403 Forbidden
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    // Shows a developer-friendly detailed error page in the browser.
    // Overrides the generic RFC-9457 error response.
    app.UseDeveloperExceptionPage();
}

app.MapErrorEndpoints();

app.Run();
