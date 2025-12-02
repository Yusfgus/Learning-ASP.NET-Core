
using Microsoft.AspNetCore.Mvc;

namespace BasicSetup.Controllers;

[ApiController]
[Route("api/product")] // [Route("api/[controller]")] // not recommended
public class ProductController : ControllerBase
{
    
    [HttpGet]
    public string Get() // Action ( public instance method )
    {
        return "Products";
    }
}