using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface IGemRepository
    {
        Task<bool> Exist(int id);

        Task<Gem> GetGemAsync(int gemId);
    }
}
