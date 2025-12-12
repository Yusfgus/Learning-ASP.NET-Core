
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// // 1
// builder.Services
//         .AddAuthentication(options =>
//         {
//             options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//             // options.DefaultAuthenticateScheme = "Cookies";
//         })
//         .AddCookie();

// // or 2
// builder.Services
//         .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//         .AddCookie();

// or 3
builder.Services
        .AddAuthentication()
        .AddCookie(); // AddJwtBearer, AddOpenId, ...


var app = builder.Build();

// Authentication middleware
// should be before any Middleware/Endpoint that depends on the user identity
// reads the cookie/header and fill HttpContext.user with it
app.UseAuthentication();

app.MapGet("/login", async (HttpContext httpContext) =>
{
    List<Claim> claims = [
        new ("name", "Issam A."),
        new ("email", "issam@localhost"),
        new ("sub", Guid.NewGuid().ToString())
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


app.MapGet("/user", (HttpContext httpContext) =>
{
    var principal = httpContext.User;

    if(principal.Identity is {IsAuthenticated: true})
    {
        var claims = principal.Claims
                    .Select(c => new
                    {
                        c.Type,
                        c.Value
                    });

        return Results.Ok(claims);
    }
    else // principal.Identity == null or IsAuthenticated == false
    {
        return Results.Unauthorized();
    }
});

app.Run();
