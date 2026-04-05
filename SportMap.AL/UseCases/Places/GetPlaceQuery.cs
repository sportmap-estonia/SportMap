using DomainLayer.Entities;
using SportMap.AL.Abstractions.UseCases;
using SportMap.AL.DTOs;

namespace SportMap.AL.UseCases.Places
{
    public record GetPlaceQuery(Guid? Id, PlaceStatus Status, Guid? PlaceTypeId) : IQuery<IReadOnlyList<PlaceDto>>;
}
