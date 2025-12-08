using ActionFilters.Filters;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace ActionFilters.Controllers;

[ApiController]
[Route("api/products")]
[TrackActionTimeFilterV3]
public class ProductController() : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        Utils.Highlight("Action Execution");

        return Ok(new[] { "Keyboard [$52.99]", "Mouse, [$34.99]" });
    }
}

