
var builder = WebApplication.CreateBuilder(args);

// DI Container ( Configuring Dependencies )

var app = builder.Build();

// Middleware setup

app.Map("/branch1", GetBranch1);

app.Map("/branch2", b2 => 
{
    GetCommonBranch(b2);
    GetBranch2(b2);
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Terminal Middleware");
});


app.Run();


static void GetCommonBranch(IApplicationBuilder b)
{
    b.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync("Middleware 1\n");
        await next();
    });
    b.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync("Middleware 2\n");
        await next();
    });
}

static void GetBranch1(IApplicationBuilder b1)
{
    GetCommonBranch(b1);
    
    b1.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync("Middleware 3\n");
        await next();
    });
    b1.Run(async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Branch1 Terminal Middleware\n");
    });
}

static void GetBranch2(IApplicationBuilder b2)
{
    b2.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync("Middleware 5\n");
        await next();
    });
    b2.Run(async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Branch2 Terminal Middleware\n");
    });
}
