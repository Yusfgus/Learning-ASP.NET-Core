
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperExceptionPage.Controllers;

[Route("error-handling")]
public class ErrorHandlerController : ControllerBase
{
    [Route("production")]
    public IActionResult ErrorProduction()
    {
        return new ObjectResult(new
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "Internal Server Error"
        });
    }

    [Route("development")]
    public IActionResult ErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        return new ObjectResult(new
        {
            details = exceptionHandlerFeature.Error.StackTrace,
            title = exceptionHandlerFeature.Error.Message
        });
    }
}