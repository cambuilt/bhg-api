using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface IIconRepository
    {
        Task<Icon> GetIconAsync(Guid id);
        Task<Icon> GetIconAsync(string name);
        Task<IconEntity> GetIconEntityAsync(Guid id);
        Task<PagedResults<Icon>> GetStringIconsAsync();
        Task<Guid> CreateIconAsync(string name, string url);
        Task UpdateIconAsync(IconEntity iconEntity);
        Task DeleteIconAsync(Guid iconId);
    }
}
