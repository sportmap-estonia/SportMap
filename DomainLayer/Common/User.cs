
namespace DomainLayer.Common
{
    public class User : BaseData
    {
        public string GoogleId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public DateOnly? Birthdate { get; set; }
        public Guid? UserRoleId { get; set; }
        public Guid? PersonalizationId { get; set; }
        public UserRole? UserRole { get; set; }
        public Personalization? Personalization { get; set; } 
    }
}