using DomainLayer.Entities;

namespace SportMap.DAL.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken cancellationToken = default);
    }
}