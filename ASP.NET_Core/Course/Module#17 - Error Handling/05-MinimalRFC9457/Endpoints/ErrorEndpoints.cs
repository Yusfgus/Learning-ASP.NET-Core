using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MinimalRFC9457.Endpoints;

public static class ErrorEndpoints
{
    public static RouteGroupBuilder MapErrorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/minimal-fake-errors");

        group.MapGet("/server-error", ServerErrorExample);

        group.MapPost("/bad-request", BadRequestExample);
        group.MapPost("/bad-request-1", BadRequestExample1);
        group.MapPost("/bad-request-2", BadRequestExample2);

        group.MapPost("/not-found", NotFoundExample);
        group.MapPost("/not-found-1", NotFoundExample1);
        group.MapPost("/not-found-2", NotFoundExample2);

        group.MapPost("/conflict", ConflictExample);
        group.MapPost("/conflict-1", ConflictExample1);
        group.MapPost("/conflict-2", ConflictExample2);

        group.MapPost("/unauthorized", () => Results.Unauthorized());

        group.MapPost("/business-rule-error", () =>
        {
            throw new ValidationException("A discontinued product cannot be put on promotion.");
        });

        return group;
    }

    public static IResult ServerErrorExample()
    {
        File.ReadAllText(@"C:\Settings\UploadSettings.json"); // not exist
        return Results.Created();
    }
    
    //==============================================================================

    private static IResult BadRequestExample()
        => Results.BadRequest();

    private static IResult BadRequestExample1()
        => Results.BadRequest("Product SKU is required");

    private static IResult BadRequestExample2() => Results.Problem(
        type: "http://example.com/prop/sku-required",
        title: "Bad Request",
        statusCode: StatusCodes.Status400BadRequest,
        detail: "Product SKU is required"
    );

    //==============================================================================

    private static IResult NotFoundExample()
        => Results.NotFound();

    private static IResult NotFoundExample1()
        => Results.NotFound("Product not found.");

    private static IResult NotFoundExample2() => Results.Problem(
        type: "http://example.com/prop/product-not-found",
        title: "Not Found",
        statusCode: StatusCodes.Status404NotFound,
        detail: "Product not found."
    );

    //==============================================================================

    private static IResult ConflictExample() 
        => Results.Conflict();

    private static IResult ConflictExample1() 
        => Results.Conflict("This Product already exists.");

    private static IResult ConflictExample2() => Results.Problem(
        type: "http://example.com/prop/product-not-found",
        title: "Conflict",
        statusCode: StatusCodes.Status409Conflict,
        detail: "This Product already exists."
    );

    //==============================================================================

    private static IResult UnauthorizedExample1()
        => Results.Unauthorized();

    //==============================================================================

    private static IResult BusinessRuleExample() 
        => throw new ValidationException("A discontinued product cannot be put on promotion.");

}

