using DomainLayer.Common;

namespace DomainLayer.Entities
{
    public class Image : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
    }
}