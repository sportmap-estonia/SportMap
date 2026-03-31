using DomainLayer.Entities;
using Microsoft.Extensions.Logging;
using SportMap.DAL.Abstractions;
using SportMap.DAL.Common;
using SportMap.DAL.DataContext;

namespace SportMap.DAL.DataAccess
{
    public class ImageRepository(AppDbContext context, ILogger<ImageRepository> logger)
        : BaseRepository<ImageData>(context, logger, context.Images), IImageRepository
    {
    }
}
