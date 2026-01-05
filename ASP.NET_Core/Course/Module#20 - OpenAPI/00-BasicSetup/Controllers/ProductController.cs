using BasicSetup.Models;
using BasicSetup.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BasicSetup.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult GetAllProducts()
    {
        return Ok(repository.GetProducts());
    }

    [HttpGet("{id:int}")]
    public ActionResult GetProductWithId([FromRoute] int id)
    {
        return Ok(repository.GetProduct(id));
    }

    [HttpPost]
    public ActionResult CreateProduct(Product product)
    {
        Console.WriteLine(product);
        repository.AddProduct(product);
        return Created();
    }
}