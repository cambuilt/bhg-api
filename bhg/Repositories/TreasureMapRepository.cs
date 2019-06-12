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

        public async Task<TreasureMapEntity> Add(TreasureMapEntity treasureMap)
        {
            await _context.TreasureMaps.AddAsync(treasureMap);
            await _context.SaveChangesAsync();
            return treasureMap;
        }

        public async Task<bool> Exist(Guid id)
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

        public async Task<TreasureMap> GetTreasureMapAsync(Guid id)
        {
            var gemEntity = new GemEntity
            {
                Id = new Guid("a62d0c34-080d-4b78-80c3-3ffde47f7ce5"),
                TreasureMapId = new Guid("301df04d-8679-4b1b-ab92-0a586ae53d08"),
                Name = "93 E Bay St",
                Description = "streetaddress",
                Address = "93 E Bay St, Charleston, SC 29401, USA",
                Latitude = 32.775797020756016,
                Longitude = -79.9272091754284,
                Notes = "",
                ImageUrl = "https://res.cloudinary.com/backyardhiddengems-com/image/upload/v1559770090/photos/93_East_Bay_St.png",
                MarkerIconUrl = "https://res.cloudinary.com/backyardhiddengems-com/image/upload/v1559583331/icons/93.svg",
                Website = null,
                CreateDate = new DateTime(2019, 6, 5, 21, 26, 0),
                ModDate = new DateTime(2019, 6, 5, 21, 29, 0)
            };

            var entity = await _context.TreasureMaps.SingleOrDefaultAsync(x => x.Id == id && x.Gems.Contains(gemEntity));

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

        public async Task<PagedResults<TreasureMap>> GetTreasureMapsAsync(
            PagingOptions pagingOptions, 
            SortOptions<TreasureMap, TreasureMapEntity> sortOptions,
            SearchOptions<TreasureMap, TreasureMapEntity> searchOptions)
        {
            IQueryable<TreasureMapEntity> query = _context.TreasureMaps;
            if (searchOptions.Search != null) { query = searchOptions.Apply(query); }
            if (sortOptions.OrderBy != null) { query = sortOptions.Apply(query); }
           
            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<TreasureMap>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<TreasureMap>
            {
                Items = items,
                TotalSize = size
            };
        }

        public async Task<TreasureMap> Remove(Guid id)
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
