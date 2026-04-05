using DomainLayer.Common;
using DomainLayer.Entities.Enums;

namespace DomainLayer.Entities
{
    public class ImageData : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public Guid UploaderId { get; set; }
        public Guid? ReviewerId { get; set; }
        public StatusType Status { get; set; }
    }
}
