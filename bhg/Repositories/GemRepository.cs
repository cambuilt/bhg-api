using bhg.Interfaces;
using bhg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace bhg.Repositories
{
    public class GemRepository : IGemRepository
    {
        private readonly BhgContext _context;
        private readonly IMapper _mapper;

        public GemRepository(BhgContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Gems.AnyAsync(c => c.Id == id);
        }

        public async Task<Gem> GetGemAsync(int Id)
        {
            var entity = await _context.Gems.SingleOrDefaultAsync(b => b.Id == Id);

            if (entity == null) return null;

            return _mapper.Map<Gem>(entity);

        }
    }
}
