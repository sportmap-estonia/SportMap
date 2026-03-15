using Microsoft.AspNetCore.Mvc;
using SportMap.AL.Abstractions;

namespace SportMap.PL.Common
{
    [Route("api/[controller]")]
    [ApiController]
    internal abstract class BaseController<TEntity>(IBaseHandler<TEntity> service, ILogger logger) : ControllerBase
        where TEntity : class, IEntity, new()
    {
        protected readonly ILogger _logger = logger;
        protected readonly IBaseHandler<TEntity> _service = service;
    }
}
