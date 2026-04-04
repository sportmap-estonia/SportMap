using DomainLayer.Entities;
using Microsoft.Extensions.Logging;
using SportMap.AL.Abstractions.Services;
using SportMap.AL.Abstractions.UseCases;
using SportMap.AL.DTOs;
using SportMap.AL.Extensions;
using SportMap.DAL.Abstractions;

namespace SportMap.AL.UseCases.Places
{
    public class GetPlaceQueryHandler(IUnitOfWork unitOfWork, ICacheService cache, ILogger<GetPlaceQueryHandler> logger) : IQueryHandler<GetPlaceQuery, IReadOnlyList<PlaceDto>>
    {
        public async Task<Result<IReadOnlyList<PlaceDto>>> Handle(GetPlaceQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<PlaceDto> places;
            logger.LogInformation("{className}.{methodName}: Trying to retrieve places", nameof(GetPlaceQueryHandler), nameof(Handle));

            try
            {
                var id = query.Id?.ToString();
                if (query.Id != null && cache.ExistsAsync(id!))
                {
                    var place = await cache.GetAsync<PlaceDto>(id!, cancellationToken);
                    return Result<IReadOnlyList<PlaceDto>>.WithData(place.AsReadonlyList());
                }

                var placeData = await unitOfWork.PlaceRepository.GetAllAsync(cancellationToken);
                var filteredPlaces = placeData
                    .Where(place => place.Status.Equals(query.Status))
                    .FilterIfNotNull(query.Id, (place, id) => place.Id == id);

                places = filteredPlaces
                    .Select(place => new PlaceDto(
                        place.Id,
                        place.Name,
                        place.Description,
                        place.PlaceTypeId,
                        place.Latitude,
                        place.Longitude,
                        place.Address,
                        place.ImageId,
                        place.CreatorId,
                        place.Creator != null ? $"{place.Creator.FirstName} {place.Creator.LastName}" : string.Empty,
                        place.CreatedAt,
                        place.ModifiedAt,
                        place.Status
                    ))
                    .ToList()
                    .AsReadOnly();
            }
            catch (OperationCanceledException oce) when (cancellationToken.IsCancellationRequested)
            {
                logger.LogWarning(oce, "{class}.{method}: Operation was canceled.", nameof(GetPlaceQueryHandler), nameof(Handle));
                return Result<IReadOnlyList<PlaceDto>>.WithError("Operation was canceled.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "{class}.{method}: Unhandled exception {message}", nameof(GetPlaceQueryHandler), nameof(Handle), e.Message);
                return Result<IReadOnlyList<PlaceDto>>.WithError(e.Message);
            }

            return Result<IReadOnlyList<PlaceDto>>.WithData(places);
        }
    }
}
