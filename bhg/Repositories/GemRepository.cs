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

        public async Task<bool> Exist(Guid id)
        {
            return await _context.Gems.AnyAsync(c => c.Id == id);
        }

        public async Task<Gem> GetGemAsync(Guid id)
        {
            var entity = await _context.Gems.SingleOrDefaultAsync(b => b.Id == id);

            if (entity == null) return null;

            return _mapper.Map<Gem>(entity);

        }

        public async Task<Guid> CreateGemAsync(
            Guid treasureMapId, string name, string description, string address, double latitude, double longitude, string notes, string imageUrl, string markerIconUrl)
        {
            var treasureMap = await _context.TreasureMaps
                .SingleOrDefaultAsync(r => r.Id == treasureMapId);
            if (treasureMap == null) throw new ArgumentException("Invalid treasure map ID.");

            var id = Guid.NewGuid();

            var newGem = _context.Gems.Add(new GemEntity
            {
                Id = id,
                TreasureMapId = treasureMapId,
                Name = name,
                Description = description,
                Address = address,
                Latitude = latitude,
                Longitude = longitude,
                Notes = notes,
                ImageUrl = imageUrl,
                MarkerIconUrl = markerIconUrl,
                CreateDate = DateTime.Now,
                ModDate = DateTime.Now
            });

            var created = await _context.SaveChangesAsync();
            if (created < 1) throw new InvalidOperationException("Could not create gem.");

            return id;
        }

        public async Task DeleteGemAsync(Guid gemId)
        {
            var booking = await _context.Gems
                .SingleOrDefaultAsync(b => b.Id == gemId);
            if (booking == null) return;

            _context.Gems.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
