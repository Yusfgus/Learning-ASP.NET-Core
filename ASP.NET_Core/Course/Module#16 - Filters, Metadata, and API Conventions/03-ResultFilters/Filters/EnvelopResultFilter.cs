using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared;

namespace ResultFilters.Filters;

// Modify the response right before sending to client.

public class EnvelopResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        Utils.Highlight("Result Filter... Started");

        if (context.Result is ObjectResult objectResult && objectResult.Value is not null)
        {
            var wrapped = new
            {
                success = true,
                data = objectResult.Value
            };

            context.Result = new JsonResult(wrapped)
            {
                StatusCode = objectResult.StatusCode
            };
        }

        await next();
        Utils.Highlight("Result Filter... Finished");
    }
}
