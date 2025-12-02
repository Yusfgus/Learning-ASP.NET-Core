
using Microsoft.AspNetCore.Mvc;

namespace RESTFulApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{

    [HttpOptions]
    public IActionResult OptionsProducts()
    {
        Response.Headers.Append("Allow", "GET, HEAD, POST, PUT PATCH DELETE, OPTIONS");

        return NoContent();
    }

}
