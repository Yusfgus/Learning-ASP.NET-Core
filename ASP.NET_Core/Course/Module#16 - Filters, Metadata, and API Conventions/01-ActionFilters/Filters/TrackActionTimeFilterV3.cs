using Microsoft.AspNetCore.Mvc.Filters;
using Shared;

namespace ActionFilters.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TrackActionTimeFilterV3 : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Utils.Highlight("Track Action Time Filter V3 ... Started");

        context.HttpContext.Items["ActionStartTime"] = DateTime.UtcNow;

        await next(); // Action Execution

        var startTime = (DateTime)context.HttpContext.Items["ActionStartTime"]!;

        var elapsed = DateTime.UtcNow - startTime;

        context.HttpContext.Response.Headers.Append("X-Elapsed-Time", $"{elapsed.TotalMilliseconds}ms");

        Utils.Highlight($"Track Action Time Filter V3 ... Took {elapsed.TotalMilliseconds}ms");
    }
}