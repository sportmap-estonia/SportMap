namespace SportMap.AL.Abstractions.Dtos
{
    public class GoogleUserInfoDto
    {
        public string GoogleId { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string? GivenName { get; init; }
        public string? FamilyName { get; init; }
        public string? Picture { get; init; }
    }
}