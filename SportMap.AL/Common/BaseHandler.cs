using DomainLayer.Common;
using Microsoft.Extensions.Logging;
using SportMap.AL.Abstractions;
using SportMap.AL.Abstractions.Services;
using SportMap.DAL.Abstractions;

namespace SportMap.AL.Common
{
    public abstract class BaseHandler<TEntity, TDTO>(IUnitOfWork unitOfWork, ICacheService cache, ILogger<BaseHandler<TEntity, TDTO>> logger) : IBaseHandler<TEntity>
        where TDTO : class, IDTO
        where TEntity : class, IEntity
    {
        protected readonly ILogger<BaseHandler<TEntity, TDTO>> _logger = logger;
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    }
}
