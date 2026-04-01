using Microsoft.AspNetCore.Mvc;
using SportMap.AL.Abstractions;

namespace SportMap.PL.Common
{
    [ApiController]
    public abstract class BaseController<TDTO>(ILogger<BaseController<TDTO>> logger) : ControllerBase
        where TDTO : class, IDTO
    {
        protected readonly ILogger _logger = logger;
    }
}
