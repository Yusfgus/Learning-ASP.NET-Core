using BasicSetup.Models;

namespace BasicSetup.Repository;

public interface IProductRepository
{
    void AddProduct(Product product);
    Product? GetProduct(int id);
    List<Product> GetProducts();
}