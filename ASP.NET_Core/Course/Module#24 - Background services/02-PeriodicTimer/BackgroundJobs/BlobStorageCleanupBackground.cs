namespace Periodic_Timer.BackgroundJobs;

public class BlobStorageCleanupBackground(ILogger<BlobStorageCleanupBackground> logger) : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(6);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Background service started");

        var periodicTimer = new PeriodicTimer(_interval);

        while (await periodicTimer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                logger.LogInformation("Scanning for orphaned blobs...");

                await Task.Delay(1000, stoppingToken);

                int OrphanedItemsCount = Random.Shared.Next(1, 10);

                logger.LogInformation("Deleted {OrphanedItemsCount} orphaned blobs at {time}", 
                                        OrphanedItemsCount, DateTimeOffset.UtcNow);     
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled error occurred");
            }
        }
    }
}