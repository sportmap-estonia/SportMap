using DomainLayer.Entities.Enums;

namespace SportMap.AL.Abstractions.Dtos
{
    public class ImageDto : IDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StatusType Status { get; set; }
    }
}
