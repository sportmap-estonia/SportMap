namespace SportMap.AL.Abstractions.Dtos
{
    public class UserInfoDto : IDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    }
}
