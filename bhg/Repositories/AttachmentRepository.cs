using bhg.Interfaces;
using bhg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace bhg.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private BhgContext _context;
        private readonly IMapper _mapper;

        public AttachmentRepository(BhgContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Attachment> GetAttachmentAsync(int Id)
        {
            var entity = await _context.Attachments.SingleOrDefaultAsync(b => b.Id == Id);

            if (entity == null) return null;

            return _mapper.Map<Attachment>(entity);
        }
    }
}
