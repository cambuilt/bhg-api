using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bhg.Models;
using bhg.Interfaces;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/[controller]")]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentsController(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        [HttpGet("{attachmentId}", Name = nameof(GetAttachmentById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Gem))]
        public async Task<ActionResult<Attachment>> GetAttachmentById([FromRoute] int id)
        {
            var attachment = await _attachmentRepository.GetAttachmentAsync(id);
            if (attachment == null) return NotFound();

            return attachment;
        }
    }
}