using DomainLayer.Entities;
using SportMap.DAL.Abstractions;

namespace SportMap.DAL.Common
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken cancellationToken = default);
    }
}