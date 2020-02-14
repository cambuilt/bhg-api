using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bhg.Models;
using bhg.Interfaces;
using bhg.Infrastructure;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/TreasureMaps/[controller]")]
    public class IconsController : ControllerBase
    {
        private readonly IIconRepository _iconRepository;
        public IconsController(IIconRepository iconRepository)
        {
            _iconRepository = iconRepository;
        }

        [HttpGet("{id}", Name = nameof(GetIconById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Icon))]
        public async Task<ActionResult<Icon>> GetIconById([FromRoute] Guid id)
        {
            var icon = await _iconRepository.GetIconAsync(id);
            if (icon == null) return NotFound();
            return icon;
        }

        [HttpGet("{name}", Name = nameof(GetIconByName))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Icon))]
        public async Task<ActionResult<Icon>> GetIconByName([FromRoute] string name)
        {
            var icon = await _iconRepository.GetIconAsync(id);
            if (icon == null) return NotFound();
            return icon;
        }

        [HttpGet(Name = nameof(GetStringIcons))]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<Icon>>> GetStringIcons() {
            PagedResults<Icon> icons = await _iconRepository.GetStringIconsAsync();
            PagingOptions pagingOptions = new PagingOptions();

            var collection = PagedCollection<Icon>.Create<IconsResponse>(
                Link.ToCollection(nameof(GetStringIcons)),
                    icons.Items.ToArray(),
                    icons.TotalSize,
                    pagingOptions) as IconsResponse;

            collection.IconsQuery = FormMetadata.FromResource<TreasureMap>(
            Link.ToForm(nameof(GetStringIcons), null, Link.GetMethod, Form.QueryRelation));

            return collection;
        }

        // POST /treasuremaps/icons
        [HttpPost(Name = nameof(CreateIconAsync))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateIconAsync(
            [FromBody] IconForm iconForm)
        {
            var nullValue = await _iconRepository.CreateIconAsync(iconForm.Name, iconForm.Url);

            return Created("", null);
        }

        // DELETE /icons/{iconId}
        [HttpDelete("{iconId}", Name = nameof(DeleteIconById))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteIconById(Guid iconId)
        {
            await _iconRepository.DeleteIconAsync(iconId);
            return NoContent();
        }

        // PUT /icons/{iconId}
        [HttpPut("{iconId}", Name = nameof(UpdateIconById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> UpdateIconById(Guid iconId, [FromBody] IconForm iconForm)
        {
            var entity = await _iconRepository.GetIconEntityAsync(iconId);
            if (entity == null) return NotFound();

            entity.Name = iconForm.Name;
            entity.Url = iconForm.Url;

            await _iconRepository.UpdateIconAsync(entity);
            return NoContent();
        }

    }
}