using HybridCaching.Models;
using HybridCaching.Requests;
using HybridCaching.Responses;

namespace HybridCaching.Services;

public interface IProductService
{
    Task<List<ProductResponse>> GetProductsAsync();

    Task<ProductResponse?> GetProductByIdAsync(int productId);

    Task<ProductResponse> AddProductAsync(CreateProductRequest request);

    Task UpdateProductAsync(int productId, UpdateProductRequest request);

    Task DeleteProductAsync(int id);
}
