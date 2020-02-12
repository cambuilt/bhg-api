using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface IRouteLineRepository
    {
        Task<DBNull> CreateRouteLineAsync(Guid treasureMapId, double startLatitude, double startLongitude, double endLatitude, double endLongitude);

    }
}
