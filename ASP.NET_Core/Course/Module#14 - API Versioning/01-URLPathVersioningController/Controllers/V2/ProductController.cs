
using URLPathVersioningController.Data;
using URLPathVersioningController.Models;
using Microsoft.AspNetCore.Mvc;
using URLPathVersioningController.Responses.V2;

namespace URLPathVersioningController.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/products")]  // /api/v2.0/products/20
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