using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared;

namespace ResourceFilters.Filters;

// Runs before model binding & action
// Good for caching, short-circuiting requests

public class TenantValidationFilter(IConfiguration config) : IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        Utils.Highlight("Tenant Validation Filter... Started");

        string tenantId = context.HttpContext.Request.Headers["tenantId"].ToString();
        string apiKey = context.HttpContext.Request.Headers["x-api-key"].ToString();

        string? expectedKey = config[$"Tenants:{tenantId}:ApiKey"];

        if (string.IsNullOrEmpty(expectedKey) || expectedKey != apiKey)
        {
            context.Result = new UnauthorizedResult();
            Utils.Highlight("Tenant Validation Filter... Unauthorized", ConsoleColor.Red);
        }
        else
        {
            await next();
            Utils.Highlight("Tenant Validation Filter... Finished");
        }
    }
}