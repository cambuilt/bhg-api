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
        public async Task<List<GemEntity>> GetGemsByLatLngAsync(double lat, double lng)
        {
            IQueryable<GemEntity> query = _context.Gems;

            foreach(var gem in _context.Gems)
            {
                var dist = gem.Name + ":" + distance(gem.Latitude, gem.Longitude, lat, lng, 'K').ToString();
                Console.WriteLine(dist);
            }

            var items = await query
                .Where(x => distance(x.Latitude, x.Longitude, lat, lng, 'K') < 0.1)
                .OrderBy(x => distance(x.Latitude, x.Longitude, lat, lng, 'K'))
                .ToListAsync();

            return items;
        }

        public async Task<Guid> CreateGemAsync(
            Guid treasureMapId, Guid iconId, string name, string description, string address, double latitude, double longitude, string notes, string imageUrl, string website)
        {
            var treasureMap = await _context.TreasureMaps
                .SingleOrDefaultAsync(r => r.Id == treasureMapId);
            if (treasureMap == null) throw new ArgumentException("Invalid treasure map ID.");

            var id = Guid.NewGuid();

            var newGem = _context.Gems.Add(new GemEntity
            {
                Id = id,
                TreasureMapId = treasureMapId,
                IconId = iconId,
                Name = name,
                Description = description,
                Address = address,
                Latitude = latitude,
                Longitude = longitude,
                Notes = notes,
                ImageUrl = imageUrl,
                Website = website,
                CreateDate = DateTime.Now,
                ModDate = DateTime.Now
            });

            var created = await _context.SaveChangesAsync();
            if (created < 1) throw new InvalidOperationException("Could not create gem.");

            return id;
        }

        public async Task UpdateGemAsync(GemEntity gemEntity)
        {
            _context.Update(gemEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGemAsync(Guid gemId)
        {
            var gem = await _context.Gems
                .SingleOrDefaultAsync(b => b.Id == gemId);
            if (gem == null) return;

            _context.Gems.Remove(gem);
            await _context.SaveChangesAsync();
        }

        private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        public async Task<GemEntity> GetGemEntityAsync(Guid id)
        {
            var entity = await _context.Gems.SingleOrDefaultAsync(b => b.Id == id);

            if (entity == null) return null;

            return entity;
        }
    }
}
