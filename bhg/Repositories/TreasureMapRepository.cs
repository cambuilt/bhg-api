using AutoMapper;
using bhg.Interfaces;
using bhg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace bhg.Repositories
{
    public class TreasureMapRepository : ITreasureMapRepository
    {
        private readonly BhgContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public TreasureMapRepository(BhgContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }
        public void Add(TreasureMap treasureMap)
        {
            _context.TreasureMaps.Add(treasureMap);            
        }
        public void Delete(TreasureMap treasureMap)
        {
            _context.TreasureMaps.Remove(treasureMap);
        }
        public async Task<TreasureMap[]> GetAllTreasureMapsAsync(bool includeGems = false)
        {
            IQueryable<TreasureMap> query = _context.TreasureMaps;

            if (includeGems)
            {
                query = query.Include(c => c.Gems);
            }

            // Order It
            query = query.OrderBy(c => c.Name);

            return await query.ToArrayAsync();
        }

        public async Task<TreasureMap> GetTreasureMapAsync(string name, bool includeGems = false)
        {
            IQueryable<TreasureMap> query = _context.TreasureMaps;

            if (includeGems)
            {
                query = query.Include(c => c.Gems);
            }

            query = query.Where(x => x.Name == name);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TreasureMap> Update(TreasureMap treasureMap)
        {
            _context.TreasureMaps.Update(treasureMap);
            await _context.SaveChangesAsync();
            return treasureMap;
        }
    }
}
