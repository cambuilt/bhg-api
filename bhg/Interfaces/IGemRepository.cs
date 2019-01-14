using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface IGemRepository
    {
        Task<bool> Exist(Guid id);

        Task<Gem> GetGemAsync(Guid id);

        Task<Guid> CreateGemAsync(
            Guid treasureMapId, string name, string description, string address, double latitude, double longitude, string notes, string imageUrl, string markerIconUrl);

        Task DeleteGemAsync(Guid gemId);
    }
}
