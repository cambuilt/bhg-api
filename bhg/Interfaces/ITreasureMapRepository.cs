using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface ITreasureMapRepository
    {
        Task<TreasureMapEntity> Add(TreasureMapEntity treasureMap);

        Task<IEnumerable<TreasureMap>> GetTreasureMapsAsync();

        Task<TreasureMap> GetTreasureMapAsync(int id);

        //Task<TreasureMap> Find(int id);

        Task<TreasureMapEntity> Update(TreasureMapEntity treasureMap);

        Task<TreasureMap> Remove(int id);

        Task<bool> Exist(int id);
    }
}
