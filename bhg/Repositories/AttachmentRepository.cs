using bhg.Interfaces;
using bhg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private BhgContext _context;

        public AttachmentRepository(BhgContext context)
        {
            _context = context;
        }

        public async Task<Attachment> Add(Attachment attachment)
        {
            await _context.Attachment.AddAsync(attachment);
            await _context.SaveChangesAsync();
            return attachment;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Attachment.AnyAsync(c => c.AttachmentId == id);
        }

        public async Task<Attachment> Find(int id)
        {
            return await _context.Attachment.Include(attachment => attachment.Place).SingleOrDefaultAsync(a => a.AttachmentId == id);
        }

        public IEnumerable<Attachment> GetAll()
        {
            return _context.Attachment;
        }

        public async Task<Attachment> Remove(int id)
        {
            var attachment = await _context.Attachment.SingleAsync(a => a.AttachmentId == id);
            _context.Attachment.Remove(attachment);
            await _context.SaveChangesAsync();
            return attachment;
        }

        public async Task<Attachment> Update(Attachment attachment)
        {
            _context.Attachment.Update(attachment);
            await _context.SaveChangesAsync();
            return attachment;
        }
    }

}
