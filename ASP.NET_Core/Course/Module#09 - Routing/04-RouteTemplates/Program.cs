
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Rout segment: /product/{id}
// Rout parameter: {id}
// Endpoint delegate: (int id) => {}
app.MapGet("/product/{id}", (int id) => $"[/product/id]: Product {id}");


// multiple rout parameters
app.MapGet("/date/{year}-{month}-{day}", 
        (int year, int month, int day) => $"[/date/year-month-day]: Date is {new DateOnly(year, month, day)}"
);


// rout parameter default value
app.MapGet("/{controller=Home}", (string? controller) => $"[/controller=Home]: {controller}");


// optional rout parameter
app.MapGet("/users/{id?}", (int? id) => id is null ? "[/users/id?]: All users" : $"User {id}");


// complex rout segment
app.MapGet("/a{b}c{d}", (string b, string d) => $"[/a(b)c(d)]: b: {b}, d: {d}");


// single catch all
app.MapGet("/single/{*slug}", (string slug) => $"[single/*slug]: Slug: {slug}");


// double catch all (differs when using Linq generation)
app.MapGet("/double/{**slug}", (string slug) => $"[/double/**slug]: Slug: {slug}");


app.Run();
