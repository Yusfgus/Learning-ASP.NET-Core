namespace LoggingConfigurationExternal.Services;

public class OrderService(ILogger<OrderService> logger)
{
    public Task ProcessOrder(Guid orderId, Guid userId)
    {
        logger.LogTrace("Trace: Entering ProcessOrder method");

        logger.LogDebug("Debug: Initializing order processing logic");

        logger.LogInformation("Order {OrderId} processing started  for user {User Id}", orderId, userId);

        logger.LogWarning("Warning: Inventory low for item XYZ");

        logger.LogError("Error: Failed to charge customer");

        logger.LogCritical("Critical: Database unavailable, shutting down");
    
        return Task.CompletedTask;
    }
}
