using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface IPlusCodeLocalRepository
    {
        Task<bool> Exist(Guid id);

        Task<PlusCodeLocal> GetPlusCodeLocalAsync(Guid id);

        Task<Guid> CreatePlusCodeLocalAsync(Guid gemId, string localCode);

        Task UpdatePlusCodeLocalAsync(PlusCodeLocalEntity plusCodeLocalEntity);

        Task DeletePlusCodeLocalAsync(Guid plusCodeLocalId);

    }
}
