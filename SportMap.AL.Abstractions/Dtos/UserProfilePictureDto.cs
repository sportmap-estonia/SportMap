namespace SportMap.AL.Abstractions.Dtos
{
    public class UserProfilePictureDto : IDTO
    {
        public Guid Id { get; set; }
        public Guid? ProfilePictureId { get; set; }
    }
}
