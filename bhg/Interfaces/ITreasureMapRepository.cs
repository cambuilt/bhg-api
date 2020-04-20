using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface ITreasureMapRepository
    {
        void Add(TreasureMap treasureMap);
        void Delete(TreasureMap treasureMap);

        Task<PagedResults<TreasureMap>> GetTreasureMapsAsync(
            PagingOptions pagingOptions, 
            SortOptions<TreasureMap, TreasureMapEntity> sortOptions,
            SearchOptions<TreasureMap, TreasureMapEntity> searchOptions);
        Task<TreasureMap> GetTreasureMapAsync(Guid id);
        Task<TreasureMap[]> GetAllTreasureMapsAsync(bool includeGems = false);
        Task<TreasureMap> Update(TreasureMapEntity treasureMap);
        Task<bool> Exist(Guid id);
    }
}
