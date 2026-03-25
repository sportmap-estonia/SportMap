using DomainLayer.Common;

namespace DomainLayer.Entities
{
    public class PrivacyType : BaseData
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}