using Microsoft.AspNetCore.Mvc;
using Shared;

namespace ResourceFilters.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController() : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        Utils.Highlight("Action Execution");

        return Ok(new[] { "Keyboard [$52.99]", "Mouse, [$34.99]" });
    }
}

