using Microsoft.Extensions.Logging;
using SportMap.AL.Abstractions;
using SportMap.AL.Abstractions.Services;
using SportMap.DAL.Abstractions;

namespace SportMap.AL.Common
{
    public abstract class BaseHandler<TEntity>(IUnitOfWork unitOfWork, ICacheService cache, ILoggerFactory factory) : IBaseHandler<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ILogger _logger = factory.CreateLogger(nameof(BaseHandler<TEntity>));
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    }
}
