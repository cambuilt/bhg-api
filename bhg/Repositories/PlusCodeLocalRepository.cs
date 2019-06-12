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
    public class PlusCodeLocalRepository : IPlusCodeLocalRepository
    {
        private readonly BhgContext _context;
        private readonly IMapper _mapper;

        public PlusCodeLocalRepository(BhgContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Exist(Guid id)
        {
            return await _context.PlusCodeLocals.AnyAsync(c => c.Id == id);
        }
        public async Task<PlusCodeLocal> GetPlusCodeLocalAsync(Guid id)
        {
            var entity = await _context.PlusCodeLocals.SingleOrDefaultAsync(b => b.Id == id);

            if (entity == null) return null;

            return _mapper.Map<PlusCodeLocal>(entity);
        }
        public async Task<Guid> CreatePlusCodeLocalAsync(Guid gemId, string localCode)
        {
            var gem = await _context.Gems
                .SingleOrDefaultAsync(r => r.Id == gemId);
            if (gem == null) throw new ArgumentException("Invalid gem ID.");
            
            var id = Guid.NewGuid();

            var newPlusCodeLocal = _context.PlusCodeLocals.Add(new PlusCodeLocalEntity
            {
                Id = id,
                GemId = gemId,
                LocalCode = localCode
            });

            var created = await _context.SaveChangesAsync();
            if (created < 1) throw new InvalidOperationException("Could not create plus code local.");

            return id;
        }
        public async Task UpdatePlusCodeLocalAsync(PlusCodeLocalEntity plusCodeLocalEntity)
        {
            _context.Update(plusCodeLocalEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlusCodeLocalAsync(Guid id)
        {
            var plusCodeLocal = await _context.PlusCodeLocals
                .SingleOrDefaultAsync(b => b.Id == id);
            if (plusCodeLocal == null) return;

            _context.PlusCodeLocals.Remove(plusCodeLocal);
            await _context.SaveChangesAsync();
        }
    }
}
