
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RFC9457.Controllers;

[Route("error-handling")]
public class ErrorHandlerController : ControllerBase
{
    [Route("production")]
    public IActionResult ErrorProduction()
    {
        var problemDetails = new ProblemDetails
        {
            Type = "https://example.com/probs/internal-server-error",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unexpected error occurred",
            Instance = HttpContext.Request.Path
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }

    [Route("development")]
    public IActionResult ErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var problemDetails = new ProblemDetails
        {
            Type = "https://example.com/probs/internal-server-error",
            Title = exception?.Message ?? "Unhandled Exception",
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception?.StackTrace ?? "No stack trace available.",
            Instance = HttpContext.Request.Path
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }
}