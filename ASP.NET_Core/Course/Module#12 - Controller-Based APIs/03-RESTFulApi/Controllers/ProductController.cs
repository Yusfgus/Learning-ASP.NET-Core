
using Microsoft.AspNetCore.Mvc;
using RESTFulApi.Data;
using RESTFulApi.Models;
using RESTFulApi.Responses;

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


    [HttpGet("{productId:guid}")]
    public ActionResult<ProductResponse> GetProductById(Guid productId, bool includeReviews = false)
    {
        Product? product = repository.GetProductById(productId);

        if(product is null)
            return NotFound();

        List<ProductReview>? reviews = includeReviews
                                       ? repository.GetProductReviews(productId) 
                                       : null;

        return ProductResponse.FromModel(product, reviews);
    }

    [HttpGet]
    public IActionResult GetPaged(int page = 1, int pageSize = 10)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        int totalCount = repository.GetProductsCount();
        List<Product> products = repository.GetProductsPage(page, pageSize);

        PagedResult<ProductResponse> pageResult = PagedResult<ProductResponse>.Create(
                                                    ProductResponse.FromModels(products),
                                                    totalCount,
                                                    page,
                                                    pageSize
                                                );

        return Ok(pageResult);
    }

}
