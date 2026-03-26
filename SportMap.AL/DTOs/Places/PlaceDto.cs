namespace SportMap.AL.DTOs.Places
{
    public class PlaceDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public Guid PlaceTypeId { get; init; }
        public PlaceTypeDto? PlaceType { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string? Address { get; init; }
        public Guid? ImageId { get; init; }
        public ImageDto? Image { get; init; }
        public Guid CreatorId { get; init; }
        public string CreatorName { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public string Status { get; init; } = string.Empty;
    }

    public class PlaceTypeDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string IconName { get; init; } = string.Empty;
    }

    public class ImageDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string FileName { get; init; } = string.Empty;
    }
}