
using HeaderController.Data;
using HeaderController.Models;
using Microsoft.AspNetCore.Mvc;
using HeaderController.Responses.V2;

namespace HeaderController.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/products")]
public class ProductController(ProductRepository repository) : ControllerBase
{

    [HttpGet("{id:guid}")]
    public ActionResult<ProductResponse> GetProduct(Guid id)
    {
        Product? product = repository.GetProductById(id);

        return product is null
                ? NotFound()
                : Ok(ProductResponse.FromModel(product));
    }
}