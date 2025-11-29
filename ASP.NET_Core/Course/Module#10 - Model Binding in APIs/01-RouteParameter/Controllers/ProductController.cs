using Microsoft.AspNetCore.Mvc;

namespace RouteParameter.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("product-controller-1/{id}")]
    public string Get1(int id)
    {
        return $"Product {id}";
    }

    [HttpGet("product-controller-2/{id}")]
    public string Get2([FromRoute(Name = "id")] int identifier)
    {
        return $"Product {identifier}";
    }

    [HttpGet("product-controller-3/{id}")]
    public string Get3([FromQuery] int id)
    {
        return $"Product {id}";
    }
}
