namespace LogLevels.Services;

public class OrderService(ILogger<OrderService> logger)
{
    public Task ProcessOrder(Guid orderId)
    {
        // logger.Log(LogLevel.Information, "Order {OrderId} processing started", orderId);
        logger.LogInformation("Order {OrderId} processing started", orderId);

        logger.LogTrace("Trace: Entering ProcessOrder method");

        logger.LogDebug("Debug: Initializing order processing logic");

        logger.LogWarning("Warning: Inventory low for item XYZ");

        logger.LogError("Error: Failed to charge customer");

        logger.LogCritical("Critical: Database unavailable, shutting down");

        return Task.CompletedTask;
    }
}
