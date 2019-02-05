using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bhg.Models;
using bhg.Interfaces;
using System;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/TreasureMaps/[controller]")]
    public class GemsController : ControllerBase
    {
        private readonly IGemRepository _gemRepository;

        public GemsController(IGemRepository gemRepository)
        {
            _gemRepository = gemRepository;
        }

        [HttpGet("{gemId}", Name = nameof(GetGemById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Gem))]
        public async Task<ActionResult<Gem>> GetGemById([FromRoute] Guid id)
        {
            var gem = await _gemRepository.GetGemAsync(id);
            if (gem == null) return NotFound();

            return gem;
        }

        // DELETE /gems/{gemId}
        [HttpDelete("{gemId}", Name = nameof(DeleteGemById))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteGemById(Guid gemId)
        {
            await _gemRepository.DeleteGemAsync(gemId);
            return NoContent();
        }

        // PUT /gems/{gemId}
        [HttpPut("{gemId}", Name = nameof(UpdateGemById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> UpdateGemById(Guid gemId, [FromBody] GemForm gemForm)
        {
            var entity = await _gemRepository.GetGemEntityAsync(gemId);
            if (entity == null) return NotFound();

            entity.Name = gemForm.Name;
            entity.Address = gemForm.Address;
            entity.Description = gemForm.Description;
            entity.Latitude = gemForm.Latitude;
            entity.Longitude = gemForm.Longitude;
            entity.Notes = gemForm.Notes;
            entity.ImageUrl = gemForm.ImageUrl;
            entity.MarkerIconUrl = gemForm.MarkerIconUrl;

            await _gemRepository.UpdateGemAsync(entity);
            return NoContent();
        }
    }
}