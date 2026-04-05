using DomainLayer.Entities;
using DomainLayer.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportMap.DAL.Abstractions.Repositories;
using SportMap.DAL.Common;
using SportMap.DAL.DataContext;

namespace SportMap.DAL.Repositories
{
    public class ImageRepository(AppDbContext context, ILogger<ImageRepository> logger)
        : BaseRepository<ImageData>(context, logger, context.Images), IImageRepository
    {
        public async Task SoftDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var rowsAffected = await context.Images
                .Where(img => img.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.RemovedAt, DateTime.UtcNow)
                    .SetProperty(e => e.Status, StatusType.Removed), ct);

            if (rowsAffected == 0)
                logger.LogWarning("{class}.{method}: 0 rows updated for image {id} — possible data inconsistency",
                    nameof(ImageRepository), nameof(SoftDeleteAsync), id);
        }
    }
}
