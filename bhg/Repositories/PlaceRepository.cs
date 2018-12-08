using bhg.Interfaces;
using bhg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private BhgContext _context;

        public PlaceRepository(BhgContext context)
        {
            _context = context;
        }

        public async Task<Place> Add(Place place)
        {
            await _context.Place.AddAsync(place);
            await _context.SaveChangesAsync();
            return place;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Place.AnyAsync(c => c.PlaceId == id);
        }

        public async Task<Place> Find(int id)
        {
            return await _context.Place.Include(place => place.TreasureMap).SingleOrDefaultAsync(a => a.PlaceId == id);
        }

        public IEnumerable<Place> GetAll()
        {
            return _context.Place;
        }

        public async Task<Place> Remove(int id)
        {
            var place = await _context.Place.SingleAsync(a => a.PlaceId == id);
            _context.Place.Remove(place);
            await _context.SaveChangesAsync();
            return place;
        }

        public async Task<Place> Update(Place place)
        {
            _context.Place.Update(place);
            await _context.SaveChangesAsync();
            return place;
        }
    }
}
