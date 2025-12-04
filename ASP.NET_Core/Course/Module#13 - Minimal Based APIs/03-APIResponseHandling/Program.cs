
using APIResponseHandling.Data;
using APIResponseHandling.Models;
using APIResponseHandling.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();


app.MapGet("/text", () => "Hello World!");

app.MapGet("/json", () => new { Message = "Hello World!" });

// ======================================================================================

app.MapGet("api/products-le-ir/{id:guid}", (Guid id, ProductRepository repository) =>
{
    Product? product = repository.GetProductById(id);
    return product is null 
            ? Results.NotFound()
            : Results.Ok(product);
});

//--------------------------------------------

app.MapGet("/api/products-mr-ir/{id:guid}", GetProductIResult);

static IResult GetProductIResult(Guid id, ProductRepository repository)
{
    var product = repository.GetProductById(id);

    return product is null
            ? Results.NotFound()
            : Results.Ok(product);
}

// ======================================================================================

app.MapGet("/api/products-le-tr/{id:guid}",
Results<Ok<Product>, NotFound> (Guid id, ProductRepository repository) =>
{
    var product = repository.GetProductById(id);

    return product is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(product);
});

//--------------------------------------------

app.MapGet("/api/products-mr-tr/{id:guid}", GetProductTypedResult);

static Results<Ok<Product>, NotFound> GetProductTypedResult(Guid id, ProductRepository repository)
{
    var product = repository.GetProductById(id);

    return product is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(product);
}


app.Run();
