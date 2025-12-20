using ResponseCaching.Requests;
using ResponseCaching.Responses;
using ResponseCaching.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Net.Http.Headers;

namespace ResponseCaching.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Client)]
    public async Task<ActionResult<PagedResult<ProductResponse>>> Get(int page, int pageSize)
    {
        var response = await productService.GetProductsAsync(page, pageSize);

        Console.WriteLine("Action visited");

        return Ok(response);
    }

    [HttpGet("{productId:int}", Name = nameof(GetById))]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByHeader = "If-None-Match")]
    public async Task<ActionResult<ProductResponse>> GetById(int productId)
    {
        var response = await productService.GetProductByIdAsync(productId);

        Console.WriteLine($"Action visited... id = {productId}");

        if (response is null)
            return NotFound($"Product with Id '{productId}' not found");

        // ===================================================================

        var eTag = GenerateEtag(response);

        if (Request.Headers.IfNoneMatch == eTag)
            return StatusCode(StatusCodes.Status304NotModified);

        Response.Headers.ETag = new EntityTagHeaderValue(eTag).ToString();

        // ===================================================================

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
    {
        var response = await productService.AddProductAsync(request);

        return CreatedAtRoute(nameof(GetById), new { productId = response.ProductId }, response);
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> Put(int productId, [FromBody] UpdateProductRequest request)
    {
        await productService.UpdateProductAsync(productId, request);
        
        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> Delete(int productId)
    {
        await productService.DeleteProductAsync(productId);

        return NoContent();
    }

    private string GenerateEtag(ProductResponse response)
    {
        string raw = $"{response.ProductId}|{response.Name}|{response.Price}";
        byte[] bytes = Encoding.UTF8.GetBytes(raw);
        byte[]? hash = SHA256.HashData(bytes);
        string? base64 = Convert.ToBase64String(hash);

        return $"\"{base64}\"";
    }
}