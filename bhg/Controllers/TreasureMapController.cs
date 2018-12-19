using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bhg.Models;
using bhg.Interfaces;
using System.Net;
using System;
using Microsoft.Extensions.Options;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/[controller]")]
    public class TreasureMapController : ControllerBase
    {
        private readonly ITreasureMapRepository _treasureMapRepository;

        public TreasureMapController(ITreasureMapRepository treasureMapRepository)
        {
            _treasureMapRepository = treasureMapRepository;
        }

        private async Task<bool> TreasureMapExists(int id)
        {
            return await _treasureMapRepository.Exist(id);
        }

        [HttpGet(Name = nameof(GetTreasureMap))]
        [Produces(typeof(DbSet<TreasureMap>))]
        [ResponseCache(Duration = 60)]
        public IActionResult GetTreasureMap()
        {
            var thisUrl = nameof(GetTreasureMap);
            System.Console.WriteLine(thisUrl);

            var results = new ObjectResult(_treasureMapRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            //Request.HttpContext.Response.Headers.Add("X-Total-Count", _treasureMapRepository.GetAll().Count().ToString());

            return results;
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<TreasureMap>> GetTreasureMapById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treasureMap = await _treasureMapRepository.Find(id);

            if (treasureMap == null)
            {
                return NotFound();
            }

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
            //    Place = treasureMap.Place
            //};

            return treasureMap;
        }

        [HttpPut("{id}")]
        [Produces(typeof(TreasureMap))]
        public async Task<ActionResult> PutTreasureMap([FromRoute] int id, [FromBody] TreasureMapEntity treasureMap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treasureMap.TreasureMapId)
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
        [Produces(typeof(TreasureMap))]
        public async Task<ActionResult> PostTreasureMap([FromBody] TreasureMapEntity treasureMap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _treasureMapRepository.Add(treasureMap);

            return CreatedAtAction("GetTreasureMap", new { id = treasureMap.TreasureMapId }, treasureMap);
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