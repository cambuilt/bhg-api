﻿using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Interfaces
{
    public interface IAttachmentRepository
    {
        Task<Attachment> GetAttachmentAsync(int Id);
    }
}
