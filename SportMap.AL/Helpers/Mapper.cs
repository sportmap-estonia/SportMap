using SportMap.AL.DTOs.Places;
using DomainLayer.Common;
using DomainLayer.Entities;
using System.Reflection;

namespace SportMap.AL.Helpers
{
    internal static class Mapper
    {
        public static T Map<T>(object source) where T : class, new()
        {
            var target = new T();
            var sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.CanWrite);
                if (targetProp != null)
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }

            return target;
        }

        public static PlaceDto MapToPlaceDto(Place place)
        {
            return new PlaceDto
            {
                Id = place.Id,
                Name = place.Name,
                Description = place.Description,
                PlaceTypeId = place.PlaceTypeId,
                PlaceType = place.PlaceType != null ? MapToPlaceTypeDto(place.PlaceType) : null,
                Latitude = place.Latitude,
                Longitude = place.Longitude,
                Address = place.Address,
                ImageId = place.ImageId,
                Image = place.Image != null ? MapToImageDto(place.Image) : null,
                CreatorId = place.CreatorId,
                CreatorName = place.Creator != null ? $"{place.Creator.FirstName} {place.Creator.LastName}" : string.Empty,
                CreatedAt = place.CreatedAt,
                UpdatedAt = place.ModifiedAt,
                Status = place.Status.ToString()
            };
        }

        public static PlaceTypeDto MapToPlaceTypeDto(PlaceType placeType)
        {
            return new PlaceTypeDto
            {
                Id = placeType.Id,
                Name = placeType.Name,
                Description = placeType.Description,
                IconName = placeType.IconName
            };
        }

        public static ImageDto MapToImageDto(Image image)
        {
            return new ImageDto
            {
                Id = image.Id,
                Name = image.Name,
                FileName = image.FileName
            };
        }
    }
}
