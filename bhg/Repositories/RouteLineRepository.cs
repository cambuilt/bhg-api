using bhg.Interfaces;
using bhg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace bhg.Repositories
{
    public class RouteLineRepository : IRouteLineRepository
    {
        private readonly BhgContext _context;
        public RouteLineRepository(BhgContext context)
        {
            _context = context;
        }

        public async Task<DBNull> CreateRouteLineAsync(Guid treasureMapId, double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            var treasureMap = await _context.TreasureMaps
                .SingleOrDefaultAsync(r => r.Id == treasureMapId);
            if (treasureMap == null) throw new ArgumentException("Invalid treasure map ID.");

            var maxSequence = await _context.RouteLines.MaxAsync(x => (int?)x.Sequence);
            if (maxSequence != null) maxSequence++; 

            var id = Guid.NewGuid();

            var newRouteLine = _context.RouteLines.Add(new RouteLineEntity
            {
                Id = id,
                TreasureMapId = treasureMapId,
                StartLatitude = startLatitude,
                StartLongitude = startLongitude,
                EndLatitude = endLatitude,
                EndLongitude = endLongitude,
                Sequence = maxSequence ?? 0
            });

            var created = await _context.SaveChangesAsync();
            if (created < 1) throw new InvalidOperationException("Could not create routeLine.");

            return DBNull.Value;
        }
    }
}
