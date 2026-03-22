using Microsoft.AspNetCore.Mvc;
using SportMap.AL.Abstractions;

namespace SportMap.PL.Common
{
    [Route("api/[controller]")]
    [ApiController]
    internal abstract class BaseController<TEntity>(IBaseHandler<TEntity> service, ILoggerFactory factory) : ControllerBase
        where TEntity : class, IEntity, new()
    {
        protected readonly ILogger _logger = factory.CreateLogger(nameof(BaseController<TEntity>));
        protected readonly IBaseHandler<TEntity> _service = service;
    }
}
