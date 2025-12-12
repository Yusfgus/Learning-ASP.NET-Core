
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services
        .AddAuthentication()
        .AddCookie();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin-and-HR", policy =>
    {
        policy.RequireRole("Admin");
        policy.RequireClaim("department", "HR");
    });
});

var app = builder.Build();

app.UseAuthentication();

// Authorization middleware
// should be after Authentication middleware
app.UseAuthorization();


app.MapGet("/login", async (
    HttpContext httpContext,
    [FromHeader] string role = "Admin",
    [FromHeader] string department = "HR") =>
{
    List<Claim> claims = [
        new ("name", "Issam A."),
        new ("email", "issam@localhost"),
        new ("sub", Guid.NewGuid().ToString()),
        new (ClaimTypes.Role, role),
        new ("department", department),
    ];

    string scheme = CookieAuthenticationDefaults.AuthenticationScheme;
    ClaimsIdentity identity = new ClaimsIdentity(claims, scheme);
    ClaimsPrincipal principal = new ClaimsPrincipal(identity); // the user
    await httpContext.SignInAsync(scheme, principal); // sets HttpContext.user
});


app.MapGet("/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(); // resets HttpContext.user
});


app.MapGet("/user-1", [Authorize] (HttpContext httpContext) =>
{
    var principal = httpContext.User;
    var claims = principal.Claims.Select(c => $"{c.Type}: {c.Value}");
    return Results.Ok(claims);
});


app.MapGet("/user-2", (HttpContext httpContext) =>
{
    var principal = httpContext.User;
    var claims = principal.Claims.Select(c => new { c.Type, c.Value });
    return Results.Ok(claims);
})
.RequireAuthorization();


// the cookies redirect to this when the user is not log in
app.MapGet("/account/login", () => "Login Page");

// ============================================================================

// Role (using Authorize annotation)
app.MapGet("/role-1", [Authorize(Roles = "Admin")] () => "Hello Admin !");

// Role (using Authorize fluent)
app.MapGet("/role-2", () => "Hello Admin/Supervisor !")
    .RequireAuthorization(a => a.RequireRole("Supervisor", "Admin"));

// Claims
app.MapGet("/claim", () => "Hello HR/Dev !")
    .RequireAuthorization(a => a.RequireClaim("department", "HR", "Dev"));

// Policy
app.MapGet("/policy", () => "Hello Admin HR !")
    .RequireAuthorization("Admin-and-HR");



app.Run();
