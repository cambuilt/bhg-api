using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Services
{
    public interface IOpeningService
    {
        Task<PagedResults<Opening>> GetOpeningsAsync(PagingOptions pagingOptions);

        Task<IEnumerable<BookingRange>> GetConflictingSlots(
            int roomId,
            DateTimeOffset start,
            DateTimeOffset end);
    }
}
