using Microsoft.AspNetCore.Mvc;

namespace QueryString.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("product-controller-1")]
    public string Get1(int page, int pageSize)
    {
        return $"Showing {pageSize} items of page # {page}";
    }

    [HttpGet("product-controller-2")]
    public string Get2([FromQuery(Name = "page")] int p, [FromQuery(Name = "pageSize")] int ps)
    {
        return $"Showing {p} items of page # {ps}";
    }

    [HttpGet("product-controller-3")]
    public SearchRequest GetComplexQuery([FromQuery] SearchRequest request)
    {
        return request;
    }

    [HttpGet("product-controller-4")]
    public string GetArray([FromQuery] int[] ids)
    {
        return string.Join(", ", ids);
    }

    [HttpGet("product-controller-5")]
    public DateRangeQuery GetCustom(DateRangeQuery dateRange)
    {
        return dateRange;
    }
}
