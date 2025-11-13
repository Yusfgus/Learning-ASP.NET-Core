
using ABC.MinimalApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// DI container service registration

builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// request pipeline

app.MapGet("/", () => "Hello World!!");

app.MapGet("/books", 
            async (IBookService service) => 
            {
                return Results.Ok(await service.GetAll());
            }
        );

app.Run();
