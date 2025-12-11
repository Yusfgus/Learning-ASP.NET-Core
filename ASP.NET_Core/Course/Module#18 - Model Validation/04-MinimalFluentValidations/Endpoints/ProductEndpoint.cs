
using MinimalFluentValidations.Requests;
using Microsoft.AspNetCore.Mvc;
using MinimalFluentValidations.Filters;

namespace MinimalFluentValidations.Endpoints;

public static class ProductController
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/products");

        group.MapPost("/", Post)
            .AddEndpointFilter<ValidationFilter<CreateProductRequest>>();

        return group;
    }

    private static IResult Post([FromBody] CreateProductRequest request)
    {
        return Results.Created($"/api/product/{Guid.NewGuid()}", request);
    }
}
