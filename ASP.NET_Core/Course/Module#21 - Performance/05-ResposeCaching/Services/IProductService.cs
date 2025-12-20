using ResponseCaching.Models;
using ResponseCaching.Requests;
using ResponseCaching.Responses;

namespace ResponseCaching.Services;

public interface IProductService
{
    Task<PagedResult<ProductResponse>> GetProductsAsync(int page, int pageSize);

    Task<ProductResponse?> GetProductByIdAsync(int productId);

    Task<ProductResponse> AddProductAsync(CreateProductRequest request);

    Task UpdateProductAsync(int productId, UpdateProductRequest request);

    Task DeleteProductAsync(int id);
}
