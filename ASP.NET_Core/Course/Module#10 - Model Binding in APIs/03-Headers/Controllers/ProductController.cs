using Microsoft.AspNetCore.Mvc;

namespace Headers.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("product-controller")]
    public string Get1([FromHeader(Name = "X-Api-Version")] string apiVersion,
                        [FromHeader] string language)
    {
        return $"Api Version: {apiVersion}\nLanguage: {language}";
    }
}
