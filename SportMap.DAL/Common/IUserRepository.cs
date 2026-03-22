using SportMap.DAL.Models;

namespace SportMap.DAL.Common
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken cancellationToken = default);
    }
}