
using Microsoft.AspNetCore.Mvc.Filters;
using Shared;

namespace ActionFilters.Filters;

// Run logic before and after an action executes.

public class SampleActionFilter : IActionFilter
{
    // before Action execution
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Utils.Highlight("Sample Action Filter Sync Before");
    }
    
    // after Action execution
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Utils.Highlight("Sample Action Filter Sync After");
    }
}

public class SampleActionFilterAsync : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Utils.Highlight("Sample Action Filter Sync Before");

        await next(); // Action Execution

        Utils.Highlight("Sample Action Filter Sync After");
    }
}