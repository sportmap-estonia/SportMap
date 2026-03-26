using SportMap.AL.DTOs.Places;
using DomainLayer.Common;
using DomainLayer.Entities;
using System.Reflection;

namespace SportMap.AL.Helpers
{
    internal static class Mapper
    {
        public static TTo Map<TFrom, TTo>(TFrom from) where TTo : new()
        {
            var to = new TTo();
            if (from is null) return to;
            foreach (var property in from.GetType().GetProperties())
            {
                var name = property.Name;
                var p = to.GetType().GetProperty(name);
                if (p is null) continue;
                var v = property.GetValue(from);
                try
                {
                    p.SetValue(to, v);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            return to;
        }
        public static TTo Map<TFrom, TTo>(TFrom from, params string[] exclude) where TTo : new()
        {
            var to = new TTo();
            if (to is null) return default;
            if (from is null) return to;
            foreach (var property in from.GetType().GetProperties())
            {
                var name = property.Name;
                if (exclude?.Contains(name) ?? false) continue;
                var p = to.GetType().GetProperty(name);
                if (p is null) continue;
                var v = property.GetValue(from);
                try
                {
                    p.SetValue(to, v);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            return to;
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
