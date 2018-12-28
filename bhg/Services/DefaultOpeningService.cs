using AutoMapper;
using bhg.Models;
using bhg.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Services
{
    public class DefaultOpeningService : IOpeningService
    {
        private readonly BhgContext _context;
        private readonly IDateLogicService _dateLogicService;
        private readonly IMapper _mapper;

        public DefaultOpeningService(
            BhgContext context,
            IDateLogicService dateLogicService,
            IMapper mapper)
        {
            _context = context;
            _dateLogicService = dateLogicService;
            _mapper = mapper;
        }

        public async Task<PagedResults<Opening>> GetOpeningsAsync(PagingOptions pagingOptions)
        {
            var treasureMaps = await _context.TreasureMaps.ToArrayAsync();
            var allOpenings = new List<Opening>();

            foreach (var treasureMap in treasureMaps)
            {
                // Generate a sequence of raw opening slots
                var allPossibleOpenings = _dateLogicService.GetAllSlots(
                        DateTimeOffset.UtcNow,
                        _dateLogicService.FurthestPossibleBooking(DateTimeOffset.UtcNow))
                    .ToArray();

                var conflictedSlots = await GetConflictingSlots(
                    treasureMap.Id,
                    allPossibleOpenings.First().StartAt,
                    allPossibleOpenings.Last().EndAt);

                // Remove the slots that have conflicts and project
                var openings = allPossibleOpenings
                    .Except(conflictedSlots, new BookingRangeComparer())
                    .Select(slot => new OpeningEntity
                    {
                        TreasureMapId = treasureMap.Id,
                        Rate = 100,
                        StartAt = slot.StartAt,
                        EndAt = slot.EndAt
                    })
                    .Select(model => _mapper.Map<Opening>(model));

                allOpenings.AddRange(openings);
            }

            var pagedOpenings = allOpenings
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);

            return new PagedResults<Opening>
            {
                Items = pagedOpenings,
                TotalSize = allOpenings.Count
            };
        }

        public async Task<IEnumerable<BookingRange>> GetConflictingSlots(
            int treasureMapId,
            DateTimeOffset start,
            DateTimeOffset end)
        {
            return await _context.Bookings
                .Where(b => b.TreasureMap.Id == treasureMapId && _dateLogicService.DoesConflict(b, start, end))
                // Split each existing booking up into a set of atomic slots
                .SelectMany(existing => _dateLogicService
                    .GetAllSlots(existing.StartAt, existing.EndAt))
                .ToArrayAsync();
        }
    }
}
