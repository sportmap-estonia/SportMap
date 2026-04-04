using DomainLayer.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SportMap.AL.DTOs;
using SportMap.AL.UseCases.Places;
using SportMap.PL.Common;
using SportMap.PL.Extensions;

namespace SportMap.PL.Controllers
{
    [Route("api/places")]
    [ApiController]
    public class PlacesController(
        GetPlaceQueryHandler getPlaces,
        CreatePlaceCommandHandler createPlaces,
        ILogger<PlacesController> logger) : BaseController<PlaceDto>(logger)
    {
        // GET: api/places
        [HttpGet]
        public async Task<Results<InternalServerError, NotFound, Ok<IReadOnlyList<PlaceDto>>>> Get()
        {
            AL.Abstractions.UseCases.Result<IReadOnlyList<PlaceDto>>? result;

            try
            {
                var query = new GetPlaceQuery(null, PlaceStatus.Approved);
                result = await getPlaces.Handle(query, CancellationToken.None);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{className}.{methodName}: Unhandled exception occured: {message}", nameof(PlacesController), nameof(Get), e.Message);
                return TypedResults.InternalServerError();
            }

            if (result.HasError)
            {
                _logger.LogError("{controllerName}.{methodName}: Error occurred while fetching places: {ErrorMessage}", nameof(PlacesController), nameof(Get), result.ErrorMessage);
                return TypedResults.InternalServerError();
            }

            var places = result.Data;

            if (places!.Count == 0)
            {
                _logger.LogWarning("{controllerName}.{methodName}: No places found", nameof(PlacesController), nameof(Get));
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(places);
        }

        // GET: api/places/{id:guid}
        [HttpGet("{id:guid}")]
        public async Task<Results<InternalServerError, NotFound, Ok<PlaceDto>>> Get(Guid id)
        {
            AL.Abstractions.UseCases.Result<IReadOnlyList<PlaceDto>> result;

            try
            {
                var query = new GetPlaceQuery(id, PlaceStatus.Approved);
                result = await getPlaces.Handle(query, CancellationToken.None);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{className}.{methodName}: Unhandled exception occured: {message}", nameof(PlacesController), nameof(Get), e.Message);
                return TypedResults.InternalServerError();
            }

            if (result.HasError)
            {
                _logger.LogError("{controllerName}.{methodName}: Error occurred while fetching place: {ErrorMessage}", nameof(PlacesController), nameof(Get), result.ErrorMessage);
                return TypedResults.InternalServerError();
            }

            var places = result.Data;

            if (places!.Count == 0)
            {
                _logger.LogWarning("{controllerName}.{methodName}: No place found", nameof(PlacesController), nameof(Get));
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(places[0]);
        }

        // POST: api/places
        [HttpPost]
        public async Task<Results<InternalServerError, BadRequest, CreatedAtRoute<PlaceDto>>> CreatePlace([FromBody] CreatePlaceRequest request)
        {
            if (request.Name.IsNullOrEmpty() || request.Description.IsNullOrEmpty())
            {
                _logger.LogWarning("Name or description is null or empty");
                return TypedResults.BadRequest();
            }

            var command = new CreatePlaceCommand(
                request.Name,
                request.Description,
                request.PlaceTypeId,
                request.Latitude,
                request.Longitude,
                request.Address,
                request.CreatorId
            );

            var result = await createPlaces.Handle(command, CancellationToken.None);

            if (result.HasError)
            {
                return TypedResults.InternalServerError();
            }

            return TypedResults.CreatedAtRoute(result.Data);
        }
    }

    public class CreatePlaceRequest
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public Guid PlaceTypeId { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string? Address { get; init; }
        public Guid CreatorId { get; init; }
    }
}
