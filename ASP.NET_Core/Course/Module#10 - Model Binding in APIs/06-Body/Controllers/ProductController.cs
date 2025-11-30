using Microsoft.AspNetCore.Mvc;

namespace Body.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpPost("/product-controller")]
    public async Task<IActionResult> Post([FromBody] ProductRequest request)
    {
        return Ok(request);
    }
}
