using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface IPlaceRepository
    {
        Task<Place> Add(Place item);

        IEnumerable<Place> GetAll();

        Task<Place> Find(int id);

        Task<Place> Remove(int id);

        Task<Place> Update(Place item);

        Task<bool> Exist(int id);
    }
}
