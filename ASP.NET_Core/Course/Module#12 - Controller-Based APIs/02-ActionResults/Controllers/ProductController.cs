
using Microsoft.AspNetCore.Mvc;
using BasicSetup;

namespace BasicSetup.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    
    [HttpGet]
    public IActionResult Get1()
    {
        return Ok(new Product("Keyboard", 20.99m));
    }
    
    [HttpGet]
    public ActionResult Get2()
    {
        return Ok(new Product("Keyboard", 20.99m));
    }
    
    [HttpGet]
    public ActionResult<Product> Get3()
    {
        return Ok(new Product("Keyboard", 20.99m));
    }
}
