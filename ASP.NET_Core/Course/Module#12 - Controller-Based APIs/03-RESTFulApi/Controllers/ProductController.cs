
using Microsoft.AspNetCore.JsonPatch;
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


    [HttpPut("{productId:guid}")]
    public IActionResult UpdateProduct(Guid productId, UpdateProductRequest request)
    {
        if(!repository.ExistsById(productId))
            return NotFound($"No product with Id '{productId}' was found");

        Product product = new Product
        {
            Name = request.Name,
            Price = request.Price ?? 0
        };

        if(!repository.UpdateProduct(product))
            return StatusCode(500, "Failed to update product");

        return NoContent();
    }


    [HttpPatch("{productId:guid}")]
    public IActionResult Patch(Guid productId, JsonPatchDocument<UpdateProductRequest>? patchDoc)
    {
        if(patchDoc is null)
            return BadRequest("Invalid patch document");

        Product? product = repository.GetProductById(productId);

        if(product is null)
            return NotFound($"No product with Id '{productId}' was found");

        UpdateProductRequest updateProduct = new UpdateProductRequest
        {
            Name = product.Name,
            Price = product.Price
        };

        patchDoc.ApplyTo(updateProduct);

        product.Name = updateProduct.Name;
        product.Price = updateProduct.Price ?? 0;

        if(!repository.UpdateProduct(product))
            return StatusCode(500, "Failed to update product");

        return NoContent();
    }


    [HttpDelete("{productId:guid}")]
    public IActionResult DeleteProduct(Guid productId)
    {
        if(!repository.ExistsById(productId))
            return NotFound($"No product with Id '{productId}' was found");

        if (!repository.DeleteProduct(productId))
            return StatusCode(500, "Failed to delete product");

        return NoContent();
    }

}
