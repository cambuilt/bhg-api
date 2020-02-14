using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bhg.Models;
using bhg.Interfaces;
using System.Net;
using System;
using Microsoft.Extensions.Options;
using bhg.Infrastructure;

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
        private readonly PagingOptions _defaultPagingOptions;

        public TreasureMapsController(ITreasureMapRepository treasureMapRepository, 
            IGemRepository gemRepository, IRouteLineRepository routeLineRepository, 
            IOptions<PagingOptions> defaultPagingOptionsWrapper)
        {
            _treasureMapRepository = treasureMapRepository;
            _gemRepository = gemRepository;
            _routeLineRepository = routeLineRepository;
            _defaultPagingOptions = defaultPagingOptionsWrapper.Value;
        }

        private async Task<bool> TreasureMapExists(Guid id)
        {
            return await _treasureMapRepository.Exist(id);
        }

        [HttpGet(Name = nameof(GetAllTreasureMaps))]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<Collection<TreasureMap>>> GetAllTreasureMaps(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<TreasureMap, TreasureMapEntity> sortOptions,
            [FromQuery] SearchOptions<TreasureMap, TreasureMapEntity> searchOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            PagedResults<TreasureMap> treasureMaps = await _treasureMapRepository.GetTreasureMapsAsync(
                pagingOptions, sortOptions, searchOptions);

            var collection = PagedCollection<TreasureMap>.Create<TreasureMapsResponse>(
                Link.ToCollection(nameof(GetAllTreasureMaps)),
                treasureMaps.Items.ToArray(),
                treasureMaps.TotalSize,
                pagingOptions) as TreasureMapsResponse;

            collection.TreasureMapsQuery = FormMetadata.FromResource<TreasureMap>(
                Link.ToForm(nameof(GetAllTreasureMaps), null, Link.GetMethod, Form.QueryRelation));

            return collection;
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
                treasureMapId, gemForm.IconId, gemForm.Name, gemForm.Description, gemForm.Address, gemForm.Latitude, gemForm.Longitude, gemForm.Notes, gemForm.ImageUrl, gemForm.Website);

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