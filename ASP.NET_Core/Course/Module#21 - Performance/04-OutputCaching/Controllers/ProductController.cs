using OutputCaching.Requests;
using OutputCaching.Responses;
using OutputCaching.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace OutputCaching.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [OutputCache(Duration = 10, VaryByQueryKeys = ["page", "pageSize"])]
    public async Task<ActionResult<PagedResult<ProductResponse>>> Get(int page, int pageSize)
    {
        var response = await productService.GetProductsAsync(page, pageSize);

        Console.WriteLine("Action visited");

        return Ok(response);
    }

    [HttpGet("{productId:int}", Name = nameof(GetById))]
    // [OutputCache(Duration = 10, VaryByRouteValueNames = ["productId"])]
    [OutputCache(PolicyName = "Single-Product")]
    public async Task<ActionResult<ProductResponse>> GetById(int productId)
    {
        var response = await productService.GetProductByIdAsync(productId);

        Console.WriteLine($"Action visited... id = {productId}");

        if (response is null)
            return NotFound($"Product with Id '{productId}' not found");

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductRequest request, IOutputCacheStore cacheStore)
    {
        var response = await productService.AddProductAsync(request);

        await cacheStore.EvictByTagAsync("products", default);

        return CreatedAtRoute(nameof(GetById), new { productId = response.ProductId }, response);
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> Put(int productId, [FromBody] UpdateProductRequest request, IOutputCacheStore cacheStore)
    {
        await productService.UpdateProductAsync(productId, request);

        await cacheStore.EvictByTagAsync("products", default);
        
        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> Delete(int productId, IOutputCacheStore cacheStore)
    {
        await productService.DeleteProductAsync(productId);

        await cacheStore.EvictByTagAsync("products", default);

        return NoContent();
    }
}