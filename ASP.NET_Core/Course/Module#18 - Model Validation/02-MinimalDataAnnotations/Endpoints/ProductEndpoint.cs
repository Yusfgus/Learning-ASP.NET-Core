
using ControllerDataAnnotations.Requests;
using Microsoft.AspNetCore.Mvc;
using MinimalDataAnnotations.Extensions;

namespace MinimalDataAnnotations.Endpoints;

public static class ProductController
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/products");

        group.MapPost("/", Post)
            .Validate<CreateProductRequest>();;

        return group;
    }

    private static IResult Post([FromBody] CreateProductRequest request)
    {
        return Results.Created($"/api/product/{Guid.NewGuid()}", request);
    }
}
