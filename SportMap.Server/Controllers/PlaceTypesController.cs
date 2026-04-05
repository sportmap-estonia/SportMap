using Microsoft.AspNetCore.Mvc;
using SportMap.DAL.DataContext;

namespace SportMap.PL.Controllers
{
    [Route("api/place-types")]
    [ApiController]
    public class PlaceTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlaceTypesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<PlaceTypeDto>> Get()
        {
            try
            {
                var placeTypes = _context.PlaceTypes.Select(pt => new PlaceTypeDto
                {
                    Id = pt.Id,
                    Name = pt.Name,
                    Description = pt.Description
                }).ToList();

                return Ok(placeTypes);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }

    public class PlaceTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
