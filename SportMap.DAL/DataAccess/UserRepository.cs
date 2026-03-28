using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportMap.DAL.Abstractions;
using SportMap.DAL.Common;
using SportMap.DAL.DataContext;

namespace SportMap.DAL.DataAccess
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context, ILogger<UserRepository> logger) : base(context, logger) {}
        public async Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(user => user.GoogleId == googleId, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"{nameof(UserRepository)}.{nameof(GetByGoogleIdAsync)}");
                throw;
            }
        }
    }
}