using Microsoft.Extensions.Logging;
using SportMap.AL.Abstractions;
using SportMap.AL.Abstractions.Services;
using SportMap.DAL.Abstractions;
using SportMap.DAL.DataAccess;

namespace SportMap.AL.Common
{
    public class BaseHandler<TEntity>(IUnitOfWork unitOfWork, ICacheService cache, ILogger logger) : IBaseHandler<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ILogger _logger = logger;
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    }
}
