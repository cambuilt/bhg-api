using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface IAttachmentRepository
    {
        Task<Attachment> Add(Attachment item);

        IEnumerable<Attachment> GetAll();

        Task<Attachment> Find(int id);

        Task<Attachment> Remove(int id);

        Task<Attachment> Update(Attachment item);

        Task<bool> Exist(int id);
    }
}
