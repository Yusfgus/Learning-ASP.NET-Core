using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ABC.Controller.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// DI container service registration
builder.Services.AddControllers();

builder.Services.AddScoped<IBookService, BookService>();


var app = builder.Build();

// request pipeline

app.Use(async (context, next) =>
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    context.Response.OnStarting(() =>
    {
        context.Response.Headers.Append("X-response-delay-ms", stopwatch.ElapsedMilliseconds.ToString());

        return Task.CompletedTask;
    });

    await next(context);
});

app.MapControllers();

app.Run();

