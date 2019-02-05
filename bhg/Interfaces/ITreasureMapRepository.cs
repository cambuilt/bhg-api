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

        Task<PagedResults<TreasureMap>> GetTreasureMapsAsync(
            PagingOptions pagingOptions, 
            SortOptions<TreasureMap, TreasureMapEntity> sortOptions,
            SearchOptions<TreasureMap, TreasureMapEntity> searchOptions);

        Task<TreasureMap> GetTreasureMapAsync(Guid id);

        //Task<TreasureMap> Find(int id);

        Task<TreasureMapEntity> Update(TreasureMapEntity treasureMap);

        Task<TreasureMap> Remove(Guid id);

        Task<bool> Exist(Guid id);
    }
}
