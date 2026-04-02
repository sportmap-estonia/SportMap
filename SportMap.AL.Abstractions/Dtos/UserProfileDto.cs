namespace SportMap.AL.Abstractions.Dtos
{
    public class UserProfileDto
    {
        public Guid Id { get; init; }
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string? LastName { get; init; }
        public DateOnly? Birthdate { get; init; }
        public string? RoleName { get; init; }
    }
}