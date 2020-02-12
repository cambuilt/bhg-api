﻿using bhg.Interfaces;
using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace bhg.Repositories
{
    public class IconRepository : IIconRepository
    {
        private readonly BhgContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;
        private readonly IMapper _mapper;
        public IconRepository(BhgContext context, IConfigurationProvider mappingConfiguration, IMapper mapper)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
            _mapper = mapper;
        }
        public async Task<Icon> GetIconAsync(Guid id)
        {
            var entity = await _context.Icons.SingleOrDefaultAsync(b => b.Id == id);

            if (entity == null) return null;

            return _mapper.Map<Icon>(entity);
        }
        public async Task<IconEntity> GetIconEntityAsync(Guid id)
        {
            var entity = await _context.Icons.SingleOrDefaultAsync(b => b.Id == id);

            if (entity == null) return null;

            return entity;
        }

        public async Task<PagedResults<Icon>> GetStringIconsAsync()
        {
            IQueryable<IconEntity> query = _context.Icons;

            var size = await query.CountAsync();
            var y = 0;

            var items = await query
                .Where(x => int.TryParse(x.Name.Replace("-", ""), out y) == false)
                .ProjectTo<Icon>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<Icon>
            {
                Items = items,
                TotalSize = size
            };
        }


        public async Task<Guid> CreateIconAsync(string name, string url)
        {
            var id = Guid.NewGuid();

            var newIcon = _context.Icons.Add(new IconEntity
            {
                Id = id,
                Name = name,
                Url = url
            });

            var created = await _context.SaveChangesAsync();
            if (created < 1) throw new InvalidOperationException("Could not create icon.");

            return id;
        }
        public async Task UpdateIconAsync(IconEntity iconEntity)
        {
            _context.Update(iconEntity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteIconAsync(Guid iconId)
        {
            var icon = await _context.Icons
                .SingleOrDefaultAsync(b => b.Id == iconId);
            if (icon == null) return;

            _context.Icons.Remove(icon);
            await _context.SaveChangesAsync();
        }
    }
}
