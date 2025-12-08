
using Shared;

namespace EndpointFilters.Filters;

public class TrackActionTimeEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        Utils.Highlight($"Track Time Filter... Started");

        var start = DateTime.UtcNow;

        var result = await next(context); // Action Execution

        var elapsed = DateTime.UtcNow - start;

        context.HttpContext.Response.Headers.Append("X-Elapsed-Time", $"{elapsed.TotalMilliseconds}ms");

        Utils.Highlight($"Track Time Filter... Took {elapsed.TotalMilliseconds}ms");

        return result;
    }
}