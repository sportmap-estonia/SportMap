using DomainLayer.Common;

namespace DomainLayer.Entities
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

}