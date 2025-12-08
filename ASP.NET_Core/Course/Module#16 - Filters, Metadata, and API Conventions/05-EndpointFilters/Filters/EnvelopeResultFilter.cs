
using Shared;

namespace EndpointFilters.Filters;

public class EnvelopeResultFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        Utils.Highlight("Envelope Result Filter... Started");

        var result = await next(context);

        Utils.Highlight("Envelope Result Filter... Finished");

        return Results.Json(new
        {
            success = true,
            data = result
        });
    }
}
