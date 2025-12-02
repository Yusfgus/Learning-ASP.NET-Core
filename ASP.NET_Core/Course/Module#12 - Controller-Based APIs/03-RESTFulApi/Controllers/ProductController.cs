
using Microsoft.AspNetCore.Mvc;
using RESTFulApi.Data;

namespace RESTFulApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(ProductRepository repository) : ControllerBase
{

    [HttpOptions]
    public IActionResult OptionsProducts()
    {
        Response.Headers.Append("Allow", "GET, HEAD, POST, PUT PATCH DELETE, OPTIONS");

        return NoContent();
    }


    [HttpHead("{productId:guid}")]
    public IActionResult HeadProducts(Guid productId)
    {
        return repository.ExistsById(productId)? Ok() : NotFound();
    }

}
