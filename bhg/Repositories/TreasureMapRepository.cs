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
        private BhgContext _context;

        public TreasureMapRepository(BhgContext context)
        {
            _context = context;
        }

        public async Task<TreasureMap> Add(TreasureMap treasureMap)
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
            return await _context.TreasureMap.Include(treasureMap => treasureMap.Place).SingleOrDefaultAsync(a => a.TreasureMapId == id);
        }

        public IEnumerable<TreasureMap> GetAll()
        {
            _context.TreasureMap.Include(treasureMap => treasureMap.Place).ToList();
            return _context.TreasureMap;
        }

        public async Task<TreasureMap> Remove(int id)
        {
            var treasureMap = await _context.TreasureMap.SingleAsync(a => a.TreasureMapId == id);
            _context.TreasureMap.Remove(treasureMap);
            await _context.SaveChangesAsync();
            return treasureMap;
        }

        public async Task<TreasureMap> Update(TreasureMap treasureMap)
        {
            _context.TreasureMap.Update(treasureMap);
            await _context.SaveChangesAsync();
            return treasureMap;
        }
    }
}
