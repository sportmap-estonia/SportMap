using DomainLayer.Entities;
using SportMap.AL.Abstractions.Dtos;

namespace SportMap.AL.UseCases.Images
{
    internal static class ImagesExtensions
    {
        extension(ImageData data)
        {
            public ImageDto Map()
            {
                return new ImageDto
                {
                    Id = data.Id,
                    Name = data.Name,
                    Status = data.Status
                };
            }
        }

        extension(ImageDto dto)
        {
            public ImageData Map()
            {
                return new ImageData
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Status = dto.Status
                };
            }
        }
    }
}
