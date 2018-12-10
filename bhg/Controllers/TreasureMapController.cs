using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bhg.Models;
using bhg.Interfaces;
using System.Net;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [Route("api/TreasureMap")]
    public class TreasureMapController : Controller
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

        [HttpGet]
        [Produces(typeof(DbSet<TreasureMap>))]
        [ResponseCache(Duration = 60)]
        public IActionResult GetTreasureMap()
        {
            var results = new ObjectResult(_treasureMapRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Total-Count", _treasureMapRepository.GetAll().Count().ToString());

            return results;
        }

        [HttpGet("{id}")]
        [Produces(typeof(TreasureMap))]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetTreasureMap([FromRoute] int id)
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

            return Ok(treasureMap);
        }

        [HttpPut("{id}")]
        [Produces(typeof(TreasureMap))]
        public async Task<IActionResult> PutTreasureMap([FromRoute] int id, [FromBody] TreasureMap treasureMap)
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
        public async Task<IActionResult> PostTreasureMap([FromBody] TreasureMap treasureMap)
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