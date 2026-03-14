using System.Linq.Expressions;
using SportMap.DAL.Models;

namespace SportMap.DAL.Common
{
    public interface IRepository<TData> where TData : BaseData
    {
        Task<TData?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<TData>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<TData>> FindAsync(Expression<Func<TData, bool>> predicate, CancellationToken ct = default);
        Task<TData> AddAsync(TData entity, CancellationToken ct = default);
        Task AddRangeAsync(IEnumerable<TData> entities, CancellationToken ct = default);
        Task Update(TData entity, CancellationToken ct = default);
        Task Remove(TData entity, CancellationToken ct = default);
        Task RemoveRange(IEnumerable<TData> entities, CancellationToken ct = default);
    }
}
