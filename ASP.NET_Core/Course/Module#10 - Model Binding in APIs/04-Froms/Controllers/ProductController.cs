using Microsoft.AspNetCore.Mvc;

namespace Forms.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("product-controller")]
    public IActionResult Get1([FromForm] string name, [FromForm] decimal price)
    {
        return Ok(new { name, price });
    }
}
