namespace SportMap.AL.Abstractions.Dtos
{
    public class AuthResponseDto
    {
        public string AccessToken { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty; // separate ticket for implementation?
        public long ExpiresAt { get; init; }
        public UserDto User { get; init; } = null!;
    }
}