using Microsoft.AspNetCore.Mvc.Filters;
using Shared;

namespace ActionFilters.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TrackActionTimeFilterV2 : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Utils.Highlight("Track Action Time Filter V2... Started");

        context.HttpContext.Items["ActionStartTime"] = DateTime.UtcNow;

        await next(); // Action Execution

        var startTime = (DateTime)context.HttpContext.Items["ActionStartTime"]!;

        var elapsed = DateTime.UtcNow - startTime;

        context.HttpContext.Response.Headers.Append("X-Elapsed-Time", $"{elapsed.TotalMilliseconds}ms");

        Utils.Highlight($"Track Action Time Filter V2... Took {elapsed.TotalMilliseconds}ms");
    }
}