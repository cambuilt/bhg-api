using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using bhg.Models;
using bhg.Interfaces;
using System;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/[controller]")]
    public class TreasureMapsController : ControllerBase
    {
        private readonly ITreasureMapRepository _treasureMapRepository;
        private readonly IGemRepository _gemRepository;
        private readonly IRouteLineRepository _routeLineRepository;
        private readonly IMapper mapper;
        private readonly PagingOptions _defaultPagingOptions;

        public TreasureMapsController(ITreasureMapRepository treasureMapRepository, 
            IGemRepository gemRepository, IRouteLineRepository routeLineRepository, 
            IOptions<PagingOptions> defaultPagingOptionsWrapper, 
            IMapper mapper)
        {
            _treasureMapRepository = treasureMapRepository;
            _gemRepository = gemRepository;
            _routeLineRepository = routeLineRepository;
            this.mapper = mapper;
            _defaultPagingOptions = defaultPagingOptionsWrapper.Value;
        }

        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _treasureMapRepository.GetAllTreasureMapsAsync();
                var mappedResult = mapper.Map<IEnumerable<TreasureMapEntity>>(result);
                return Ok(mappedResult);
            } catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }

        [HttpGet("{id}", Name = nameof(GetTreasureMapById))]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<TreasureMap>> GetTreasureMapById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treasureMap = await _treasureMapRepository.GetTreasureMapAsync(id);
            if (treasureMap == null) return NotFound();

           return treasureMap;
        }

        // POST /treasuremaps/{treasureMapId}/gems
        [HttpPost("{treasureMapId}/gems", Name = nameof(CreateGemForTreasureMapAsync))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateGemForTreasureMapAsync(
            Guid treasureMapId, [FromBody] GemForm gemForm)
        {
            var treasureMap = await _treasureMapRepository.GetTreasureMapAsync(treasureMapId);
            if (treasureMap == null) return NotFound();

            if (gemForm.Latitude == 0 || gemForm.Longitude == 0)
            {
                return BadRequest(new ApiError("Latitude and longitude coordinates are required."));
            }

            var gemId = await _gemRepository.CreateGemAsync(
                treasureMapId, gemForm.IconId, gemForm.Name, gemForm.Description, gemForm.Address, gemForm.Latitude, gemForm.Longitude, gemForm.Notes, gemForm.ImageUrl, gemForm.Website, gemForm.Phone, gemForm.YelpUrl, gemForm.GoogleUrl, gemForm.MenuUrl);

            return Created(
                Url.Link(nameof(GemsController.GetGemById), new { id = gemId }), gemId);
        }

        // POST /treasuremaps/{treasureMapId}/routeLines
        [HttpPost("{treasureMapId}/routeLines", Name = nameof(CreateRouteLineForTreasureMapAsync))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateRouteLineForTreasureMapAsync(
            Guid treasureMapId, [FromBody] RouteLineForm routeLineForm)
        {
            var treasureMap = await _treasureMapRepository.GetTreasureMapAsync(treasureMapId);
            if (treasureMap == null) return NotFound();

            var nullValue = await _routeLineRepository.CreateRouteLineAsync(
                treasureMapId, routeLineForm.StartLatitude, routeLineForm.StartLongitude, routeLineForm.EndLatitude, routeLineForm.EndLongitude);

            return Created("", null);
        }
    }
}