using DomainLayer.Entities;

namespace SportMap.DAL.Abstractions.Repositories
{
    public interface IImageRepository : IRepository<ImageData>
    {
        Task SoftDeleteAsync(Guid id, CancellationToken ct = default);
    }
}
