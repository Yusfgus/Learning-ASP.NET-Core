
using ControllerFluentValidations.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ControllerFluentValidations.Controllers;

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
