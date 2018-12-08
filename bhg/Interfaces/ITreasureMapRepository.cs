using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface ITreasureMapRepository
    {
        Task<TreasureMap> Add(TreasureMap treasureMap);

        IEnumerable<TreasureMap> GetAll();

        Task<TreasureMap> Find(int id);

        Task<TreasureMap> Update(TreasureMap treasureMap);

        Task<TreasureMap> Remove(int id);

        Task<bool> Exist(int id);
    }
}
