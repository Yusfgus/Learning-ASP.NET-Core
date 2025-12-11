
using System.Text.Json.Serialization;
using MinimalFluentValidations.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MinimalFluentValidations.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

var app = builder.Build();

app.MapProductEndpoints();

app.Run();
