namespace TraditionalTimer.BackgroundJobs;

public class BlobStorageCleanupBackground(ILogger<BlobStorageCleanupBackground> logger) : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(6);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Background service started");

        while (!stoppingToken.IsCancellationRequested)
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

            await Task.Delay(_interval, stoppingToken);
        }
    }
}