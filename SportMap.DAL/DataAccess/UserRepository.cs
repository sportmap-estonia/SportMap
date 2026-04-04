using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportMap.DAL.Abstractions;
using SportMap.DAL.Common;
using SportMap.DAL.DataContext;

namespace SportMap.DAL.DataAccess
{
    public class UserRepository(AppDbContext context, ILogger<UserRepository> logger) : BaseRepository<User>(context, logger, context.Users), IUserRepository
    {
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

        public async Task<User?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(user => user.UserName == username, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"{nameof(UserRepository)}.{nameof(GetByUserNameAsync)}");
                throw;
            }
        }
    }
}