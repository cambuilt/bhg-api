using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bhg.Models;
using bhg.Interfaces;

namespace bhg.Controllers
{
    [Produces("application/json")]
    [Route("api/Attachment")]
    public class AttachmentController : Controller
    {
        private readonly BhgContext _context;

        public AttachmentController(BhgContext context)
        {
            _context = context;
        }

        private bool AttachmentExists(int id)
        {
            return _context.Attachment.Any(e => e.AttachmentId == id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Attachment>))]
        public IActionResult GetAttachment()
        {
            return new ObjectResult(_context.Attachment);
        }

        [HttpGet("{id}")]
        [Produces(typeof(Attachment))]
        public async Task<IActionResult> GetAttachment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attachment = await _context.Attachment.SingleOrDefaultAsync(m => m.AttachmentId == id);

            if (attachment == null)
            {
                return NotFound();
            }

            return Ok(attachment);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Attachment))]
        public async Task<IActionResult> PutAttachment([FromRoute] int id, [FromBody] Attachment attachment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attachment.AttachmentId)
            {
                return BadRequest();
            }

            _context.Entry(attachment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(attachment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttachmentExists(id))
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
        [Produces(typeof(Attachment))]
        public async Task<IActionResult> PostAttachment([FromBody] Attachment attachment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Attachment.Add(attachment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttachment", new { id = attachment.AttachmentId }, attachment);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Attachment))]
        public async Task<IActionResult> DeleteAttachment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attachment = await _context.Attachment.SingleOrDefaultAsync(m => m.AttachmentId == id);
            if (attachment == null)
            {
                return NotFound();
            }

            _context.Attachment.Remove(attachment);
            await _context.SaveChangesAsync();

            return Ok(attachment);
        }
    }
}