using DomainLayer.Common;

namespace DomainLayer.Entities
{
    public class PrivacyType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}