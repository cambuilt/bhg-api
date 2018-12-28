﻿using AutoMapper;
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

        public async Task<TreasureMapEntity> Add(TreasureMapEntity treasureMap)
        {
            await _context.TreasureMaps.AddAsync(treasureMap);
            await _context.SaveChangesAsync();
            return treasureMap;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.TreasureMaps.AnyAsync(c => c.Id == id);
        }

        //public async Task<TreasureMap> Find(int id)
        //{
            //var treasureMap = await _context.TreasureMap.Include(tm => tm.Gem).SingleOrDefaultAsync(a => a.TreasureMapId == id);

            //if (treasureMap == null)
            //{
            //    return null;
            //}

            //return _mapper.Map<TreasureMap>(treasureMap);
        //}

        public async Task<TreasureMap> GetTreasureMapAsync(int id)
        {
            var entity = await _context.TreasureMaps.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<TreasureMap>(entity);
        }

        public IEnumerable<TreasureMapEntity> GetAll()
        {
            //_context.TreasureMap.Include(treasureMap => treasureMap.Gem).ToList();    // Eager loading of gems
            return _context.TreasureMaps;
        }

        public async Task<IEnumerable<TreasureMap>> GetTreasureMapsAsync()
        {
            var query = _context.TreasureMaps.ProjectTo<TreasureMap>(_mappingConfiguration);

            return await query.ToArrayAsync();
        }

        public async Task<TreasureMap> Remove(int id)
        {
            var entity = await _context.TreasureMaps.SingleAsync(a => a.Id == id);

            if (entity == null)
            {
                return null;
            }

            _context.TreasureMaps.Remove(entity);
            await _context.SaveChangesAsync();
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<TreasureMap>(entity);
        }

        public async Task<TreasureMapEntity> Update(TreasureMapEntity treasureMap)
        {
            _context.TreasureMaps.Update(treasureMap);
            await _context.SaveChangesAsync();
            return treasureMap;
        }
    }
}
