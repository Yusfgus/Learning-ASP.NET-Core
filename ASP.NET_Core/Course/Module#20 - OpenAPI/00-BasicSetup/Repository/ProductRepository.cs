using BasicSetup.Models;

namespace BasicSetup.Repository;

public class ProductRepository : IProductRepository
{
    private static List<Product> _products = 
    [
        new() {Id = 1, Name = "Product1", Price = 99.9m, Amount = 30},
        new() {Id = 2, Name = "Product2", Price = 55.9m, Amount = 20},
        new() {Id = 3, Name = "Product3", Price = 353, Amount = 10},
        new() {Id = 4, Name = "Product4", Price = 238, Amount = 33},
        new() {Id = 5, Name = "Product5", Price = 23m, Amount = 80},
    ];

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public Product? GetProduct(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public List<Product> GetProducts()
    {
        return _products;
    }
}
