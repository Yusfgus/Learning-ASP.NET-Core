using HeaderMinimal.Models;

namespace HeaderMinimal.Data;

public class ProductRepository
{
    private List<Product> _products =
    [
       new Product { Id = Guid.Parse("2779ee47-10b0-4bd7-8342-404006aa1392"), Name = "Soda", Price = 1.99m },
       new Product { Id = Guid.Parse("27a65726-a07f-484c-9b0c-334611ec1c18"), Name = "Milk", Price = 3.49m },
       new Product { Id = Guid.Parse("69a0b1fe-3c20-4a4a-bc57-13a8078d8e00"), Name = "Juice", Price = 4.75m },
       new Product { Id = Guid.Parse("8fa9f2a4-1b8a-4e66-ae9b-1234567890ab"), Name = "Bread", Price = 2.49m },
    ];

    public Product? GetProductById(Guid productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);

        if (product is null)
            return null;

        return product;
    }

}