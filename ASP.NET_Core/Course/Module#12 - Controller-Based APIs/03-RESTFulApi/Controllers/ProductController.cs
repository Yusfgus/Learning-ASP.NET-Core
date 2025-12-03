
using Microsoft.AspNetCore.Mvc;
using RESTFulApi.Data;
using RESTFulApi.Models;
using RESTFulApi.Requests;
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


    [HttpGet("{productId:guid}", Name = "GetProductById")]
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


    [HttpPost]
    public IActionResult CreateProduct(CreateProductRequest request)
    {
        if (repository.ExistsByName(request.Name))
            return Conflict($"A product with the same name '{request.Name}' already exists.");

        Product product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Id = Guid.NewGuid()
        };

        repository.AddProduct(product);

        return CreatedAtRoute(routeName: "GetProductById",
                            routeValues: new { productId = product.Id},
                            value: ProductResponse.FromModel(product)
                        );
    }

}
