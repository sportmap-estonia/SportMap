using Microsoft.AspNetCore.Mvc;
using SportMap.AL.Abstractions.Services;

namespace SportMap.PL.Common
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("google/callback")]
        public async Task<IActionResult> GoogleCallback([FromBody] GoogleCallbackRequest request, CancellationToken cancellationToken)
        {
            var result = await authService.GoogleCallbackAsync(request.Code, request.CodeVerifier, cancellationToken);
            return Ok(result);
        }

        public record GoogleCallbackRequest(string Code, string CodeVerifier);
    }
}