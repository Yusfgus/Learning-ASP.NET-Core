using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RFC9457Controller.Controllers;

[Route("api/controller-fake-errors")]
[ApiController]
public class FakeErrorController : ControllerBase
{
    [HttpGet("server-error")]
    public IActionResult ServerErrorExample()
    {
        System.IO.File.ReadAllText(@"C:\Settings\SomeSettings.json"); // not exist
        return Ok();
    }

    //==============================================================================

    [HttpPost("bad-request")]
    public IActionResult BadRequestExample()
        => BadRequest();

    [HttpPost("bad-request-1")]
    public IActionResult BadRequestExample1()
        => BadRequest("Product SKU is required");

    [HttpPost("bad-request-2")]
    public IActionResult BadRequestExample2() => Problem(
        type: "http://example.com/prop/sku-required",
        title: "Bad Request",
        statusCode: StatusCodes.Status400BadRequest,
        detail: "Product SKU is required"
    );

    //==============================================================================

    [HttpPost("not-found")]
    public IActionResult NotFoundExample()
        => NotFound();

    [HttpPost("not-found-1")]
    public IActionResult NotFoundExample1()
        => NotFound("Product not found.");

    [HttpPost("not-found-2")]
    public IActionResult NotFoundExample2() => Problem(
        type: "http://example.com/prop/product-not-found",
        title: "Not Found",
        statusCode: StatusCodes.Status404NotFound,
        detail: "Product not found."
    );

    //==============================================================================

    [HttpPost("conflict")]
    public IActionResult ConflictExample() 
        => Conflict();

    [HttpPost("conflict-1")]
    public IActionResult ConflictExample1() 
        => Conflict("This Product already exists.");

    [HttpPost("conflict-2")]
    public IActionResult ConflictExample2() => Problem(
        type: "http://example.com/prop/product-not-found",
        title: "Conflict",
        statusCode: StatusCodes.Status409Conflict,
        detail: "This Product already exists."
    );

    //==============================================================================

    [HttpPost("unauthorized")]
    public IActionResult UnauthorizedExample1()
        => Unauthorized();

    [HttpPost("unauthorized-1")]
    public IActionResult UnauthorizedExample()
        => Unauthorized("You are not authorized");

    [HttpPost("unauthorized-2")]
    public IActionResult UnauthorizedExample2() => Unauthorized(new ProblemDetails
    {
        Title = "You are not authorized"
    });

    //==============================================================================

    [HttpPost("business-rule-error")]
    public IActionResult BusinessRuleExample() 
        => throw new ValidationException("A discontinued product cannot be put on promotion.");

}