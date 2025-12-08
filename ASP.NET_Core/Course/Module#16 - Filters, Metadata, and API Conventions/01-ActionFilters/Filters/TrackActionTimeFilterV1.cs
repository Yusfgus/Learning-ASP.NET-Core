using Microsoft.AspNetCore.Mvc.Filters;
using Shared;

namespace ActionFilters.Filters;

public class TrackActionTimeFilterV1 : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Utils.Highlight("Track Action Time Filter V1 ... Started");

        context.HttpContext.Items["ActionStartTime"] = DateTime.UtcNow;

        await next(); // Action Execution

        var startTime = (DateTime)context.HttpContext.Items["ActionStartTime"]!;

        var elapsed = DateTime.UtcNow - startTime;

        context.HttpContext.Response.Headers.Append("X-Elapsed-Time", $"{elapsed.TotalMilliseconds}ms");

        Utils.Highlight($"Track Action Time Filter V1... Took {elapsed.TotalMilliseconds}ms");
    }
}