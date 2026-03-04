using API.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Application.IAssemblyMarker).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Application.IAssemblyMarker).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Application.Behaviors.ValidationBehavior<,>));

builder.Services.AddDbContext<Infrastructure.Data.AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

builder.Services.AddScoped<Application.Common.Interfaces.IAppDbContext, Infrastructure.Data.AppDbContext>();

var app = builder.Build();

app.UseExceptionHandler();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
