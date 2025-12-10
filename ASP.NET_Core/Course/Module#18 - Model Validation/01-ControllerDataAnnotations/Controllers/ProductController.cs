
using ControllerDataAnnotations.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ControllerDataAnnotations.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(CreateProductRequest request)
    {
        return Created($"/api/product/{Guid.NewGuid()}", request);
    }
}
