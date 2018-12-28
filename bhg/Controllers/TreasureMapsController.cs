using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bhg.Models;
using bhg.Interfaces;
using bhg.Services;
using System.Net;
using System;
using Microsoft.Extensions.Options;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/[controller]")]
    public class TreasureMapsController : ControllerBase
    {
        private readonly ITreasureMapRepository _treasureMapRepository;
        //private readonly IOpeningService _openingService;

        public TreasureMapsController(ITreasureMapRepository treasureMapRepository)
        {
            _treasureMapRepository = treasureMapRepository;
            //, IOpeningService openingService
            //_openingService = openingService;
        }

        private async Task<bool> TreasureMapExists(int id)
        {
            return await _treasureMapRepository.Exist(id);
        }

        [HttpGet(Name = nameof(GetAllTreasureMaps))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<Collection<TreasureMap>>> GetAllTreasureMaps()
        {
            IEnumerable<TreasureMap> treasureMaps = await _treasureMapRepository.GetTreasureMapsAsync();

            var collection = new Collection<TreasureMap>
            {
                Self = Link.ToCollection(nameof(GetAllTreasureMaps)),
                Value = treasureMaps.ToArray()
            };

            //Request.HttpContext.Response.Headers.Add("X-Total-Count", _treasureMapRepository.GetAll().Count().ToString());

            return collection;
        }

        [HttpGet("{id}", Name = nameof(GetTreasureMapById))]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<TreasureMap>> GetTreasureMapById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treasureMap = await _treasureMapRepository.GetTreasureMapAsync(id);
            if (treasureMap == null) return NotFound();

            // treasureMap.Href

            //var resource = new TreasureMap
            //{
            //    Href = Url.Link(nameof(GetTreasureMapById), new { id = treasureMap.TreasureMapId }),
            //    Name = treasureMap.Name,
            //    Area = treasureMap.Area,
            //    Author = treasureMap.Author,
            //    Latitude = treasureMap.Latitude,
            //    LatitudeDelta = treasureMap.LatitudeDelta,
            //    Longitude = treasureMap.Longitude,
            //    LongitudeDelta = treasureMap.LongitudeDelta,
            //    CreateDate = treasureMap.CreateDate,
            //    ModDate = treasureMap.ModDate,
            //    Gem = treasureMap.Gem
            //};

            return treasureMap;
        }

        // GET /TreasureMaps/Openings
        //[HttpGet("openings", Name = nameof(GetAllTreasureMapOpenings))]
        //[ProducesResponseType(200)]
        //public async Task<ActionResult<Collection<Opening>>> GetAllTreasureMapOpenings([FromQuery] PagingOptions pagingOptions = null)
        //{
        //    var openings = await _openingService.GetOpeningsAsync(pagingOptions);

        //    var collection = new PagedCollection<Opening>()
        //    {
        //        Self = Link.ToCollection(nameof(GetAllTreasureMapOpenings)),
        //        Value = openings.Items.ToArray(),
        //        Size = openings.TotalSize,
        //        Offset = pagingOptions.Offset.Value,
        //        Limit = pagingOptions.Limit.Value
        //    };

        //    return collection;
        //}

        [HttpPut("{id}")]
        //[Produces(typeof(TreasureMap))]
        public async Task<ActionResult> PutTreasureMap([FromRoute] int id, [FromBody] TreasureMapEntity treasureMap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treasureMap.Id)
            {
                return BadRequest();
            }

            try
            {
                await _treasureMapRepository.Update(treasureMap);
                return Ok(treasureMap);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TreasureMapExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        //[Produces(typeof(TreasureMap))]
        public async Task<ActionResult> PostTreasureMap([FromBody] TreasureMapEntity treasureMap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _treasureMapRepository.Add(treasureMap);

            return CreatedAtAction("GetTreasureMap", new { id = treasureMap.Id }, treasureMap);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(TreasureMap))]
        public async Task<IActionResult> DeleteTreasureMap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await TreasureMapExists(id))
            {
                return NotFound();
            }

            await _treasureMapRepository.Remove(id);

            return Ok();
        }
    }
}