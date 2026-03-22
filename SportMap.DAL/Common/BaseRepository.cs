using DomainLayer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportMap.DAL.Abstractions;
using SportMap.DAL.DataContext;
using System.Linq.Expressions;

namespace SportMap.DAL.Common
{
    public abstract class BaseRepository<TData> : IRepository<TData> where TData : BaseData
    {
        private readonly AppDbContext _context;
        protected readonly ILogger _logger;
        protected readonly DbSet<TData> _dbSet;

        protected BaseRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<TData>();
        }

        public async Task<TData?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var entity = await _dbSet.FindAsync(new[] { id }, ct).ConfigureAwait(false);

                if (entity is null)
                {
                    _logger.LogInformation($"{nameof(BaseRepository<TData>)}.{nameof(GetByIdAsync)}: Entity was not found");
                }

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseRepository<TData>)}.{nameof(GetByIdAsync)}");
                throw;
            }
        }

        public async Task<IReadOnlyList<TData>> GetAllAsync(CancellationToken ct = default)
        {
            try
            {
                var entities = await _dbSet.AsNoTracking().ToListAsync(ct);
                return entities.AsReadOnly();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseRepository<TData>)}.{nameof(GetAllAsync)}");
                throw;
            }
        }

        public async Task<IReadOnlyList<TData>> FindAsync(Expression<Func<TData, bool>> predicate, CancellationToken ct = default)
        {
            try
            {
                var entities = await _dbSet.AsNoTracking().Where(predicate).ToListAsync(ct);
                return entities.AsReadOnly();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseRepository<TData>)}.{nameof(FindAsync)}");
                throw;
            }
        }

        public async Task<TData> AddAsync(TData entity, CancellationToken ct = default)
        {
            try
            {
                await _dbSet.AddAsync(entity, ct);
                await _context.SaveChangesAsync(ct);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseRepository<TData>)}.{nameof(AddAsync)}");
                throw;
            }
        }

        public async Task AddRangeAsync(IEnumerable<TData> entities, CancellationToken ct = default)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities, ct);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseRepository<TData>)}.{nameof(AddRangeAsync)}");
                throw;
            }
        }

        public async Task Update(TData entity, CancellationToken ct = default)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseRepository<TData>)}.{nameof(Update)}");
                throw;
            }
        }

        public async Task Remove(TData entity, CancellationToken ct = default)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseRepository<TData>)}.{nameof(Remove)}");
                throw;
            }
        }

        public async Task RemoveRange(IEnumerable<TData> entities, CancellationToken ct = default)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BaseRepository<TData>)}.{nameof(RemoveRange)}");
                throw;
            }
        }
    }
}
