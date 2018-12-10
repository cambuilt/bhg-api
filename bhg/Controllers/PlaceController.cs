using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bhg.Models;
using bhg.Interfaces;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [Route("api/Place")]
    public class PlaceController : Controller
    {
        private readonly BhgContext _context;

        public PlaceController(BhgContext context)
        {
            _context = context;
        }

        private bool PlaceExists(int id)
        {
            return _context.Place.Any(e => e.PlaceId == id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Place>))]
        public IActionResult GetPlace()
        {
            return new ObjectResult(_context.Place);
        }

        [HttpGet("{id}")]
        [Produces(typeof(Place))]
        public async Task<IActionResult> GetPlace([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var place = await _context.Place.SingleOrDefaultAsync(m => m.PlaceId == id);

            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Place))]
        public async Task<IActionResult> PutPlace([FromRoute] int id, [FromBody] Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.PlaceId)
            {
                return BadRequest();
            }

            _context.Entry(place).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(place);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
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
        [Produces(typeof(Place))]
        public async Task<IActionResult> PostPlace([FromBody] Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Place.Add(place);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlace", new { id = place.PlaceId }, place);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Place))]
        public async Task<IActionResult> DeletePlace([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var place = await _context.Place.SingleOrDefaultAsync(m => m.PlaceId == id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Place.Remove(place);
            await _context.SaveChangesAsync();

            return Ok(place);
        }
    }
}