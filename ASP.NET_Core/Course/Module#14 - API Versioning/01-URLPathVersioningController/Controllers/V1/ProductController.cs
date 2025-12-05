
using URLPathVersioningController.Data;
using URLPathVersioningController.Models;
using Microsoft.AspNetCore.Mvc;
using URLPathVersioningController.Responses.V1;

namespace URLPathVersioningController.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/products")]  // /api/products/20
[Route("api/v{version:apiVersion}/products")]  // /api/v1.0/products/20
public class ProductController(ProductRepository repository) : ControllerBase
{

    [HttpGet("{id:guid}")]
    public ActionResult<ProductResponse> GetProduct(Guid id)
    {
        AlertClient(); // alert the client that this version will be deprecated soon.
        
        Product? product = repository.GetProductById(id);

        return product is null
                ? NotFound()
                : Ok(ProductResponse.FromModel(product));
    }

    private void AlertClient()
    {
        Response.Headers["Deprecation"] = "true";
        Response.Headers["Sunset"] = "Wed, 31 Dec 2025 23:59:59 GMT";
        Response.Headers["Link"] = "</api/v2/products>; rel=\"successor-version\"";
    }
}