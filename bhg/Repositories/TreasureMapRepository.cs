using AutoMapper;
using bhg.Interfaces;
using bhg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Repositories
{
    public class TreasureMapRepository : ITreasureMapRepository
    {
        private readonly BhgContext _context;
        private readonly IMapper _mapper;

        public TreasureMapRepository(BhgContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TreasureMapEntity> Add(TreasureMapEntity treasureMap)
        {
            await _context.TreasureMap.AddAsync(treasureMap);
            await _context.SaveChangesAsync();
            return treasureMap;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.TreasureMap.AnyAsync(c => c.TreasureMapId == id);
        }

        public async Task<TreasureMap> Find(int id)
        {
            var treasureMap = await _context.TreasureMap.Include(tm => tm.Place).SingleOrDefaultAsync(a => a.TreasureMapId == id);

            if (treasureMap == null)
            {
                return null;
            }

            return _mapper.Map<TreasureMap>(treasureMap);
        }

        public IEnumerable<TreasureMapEntity> GetAll()
        {
            //_context.TreasureMap.Include(treasureMap => treasureMap.Place).ToList();    // Eager loading of places
            return _context.TreasureMap;
        }

        public async Task<TreasureMap> Remove(int id)
        {
            var treasureMap = await _context.TreasureMap.SingleAsync(a => a.TreasureMapId == id);
            _context.TreasureMap.Remove(treasureMap);
            await _context.SaveChangesAsync();
            return _mapper.Map<TreasureMap>(treasureMap);
        }

        public async Task<TreasureMapEntity> Update(TreasureMapEntity treasureMap)
        {
            _context.TreasureMap.Update(treasureMap);
            await _context.SaveChangesAsync();
            return treasureMap;
        }
    }
}
