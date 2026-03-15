using Microsoft.Extensions.Logging;
using SportMap.AL.Abstractions;
using SportMap.DAL.DataAccess;

namespace SportMap.AL.Common
{
    public class BaseHandler<TEntity>(UnitOfWork unitOfWork, ILogger logger) : IBaseHandler<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ILogger _logger = logger;
        protected readonly UnitOfWork _unitOfWork = unitOfWork;
    }
}
