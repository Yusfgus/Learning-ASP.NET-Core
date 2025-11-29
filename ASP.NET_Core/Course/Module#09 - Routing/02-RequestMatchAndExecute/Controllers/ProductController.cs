
using Microsoft.AspNetCore.Mvc;

namespace Routing.RequestMatchAndExecute.Controllers;


[ApiController]
[Route("[controller]")] // ../products
public class ProductsController: ControllerBase
{
    // ../products/all
    [HttpGet("all")]
    public IActionResult GetProduct()
    {
        return Ok(new[]
        {
            "Product 4",
            "Product 5",
            "Product 6",
        });
    }
}