using Microsoft.AspNetCore.Mvc;
using SportMap.AL.Abstractions.Dtos;
using SportMap.AL.Abstractions.Services;

namespace SportMap.PL.Common
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController(
        IProfileService profileService,
        ILogger<ProfileController> logger) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserProfileDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var profile = await profileService.GetByIdAsync(id, cancellationToken);

            if (profile is null)
            {
                logger.LogWarning("{controller}.{method}: User {Id} not found",
                    nameof(ProfileController), nameof(Get), id);
                return NotFound();
            }

            return Ok(profile);
        }
    }
}