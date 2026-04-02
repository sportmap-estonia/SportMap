
using DomainLayer.Common;

namespace DomainLayer.Entities
{
    public class User : BaseEntity
    {
        public string GoogleId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
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