using Microsoft.AspNetCore.Mvc;

namespace Cookies.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("cookie-controller")]
    public IActionResult Get()
    {
        return Ok(HttpContext.Request.Cookies["session-id"]);
    }
}
