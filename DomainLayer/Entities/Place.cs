using DomainLayer.Common;

namespace DomainLayer.Entities
{
    public class Place : BaseData
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid PlaceTypeId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Address { get; set; }
        public Guid? ImageId { get; set; }
        public Guid CreatorId { get; set; }
        public PlaceStatus Status { get; set; } = PlaceStatus.Pending;
        public Guid? ReviewerId { get; set; }

        public PlaceType? PlaceType { get; set; }
        public Image? Image { get; set; }
        public User Creator { get; set; } = null!;
        public User? Reviewer { get; set; }
    }

    public enum PlaceStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }
}