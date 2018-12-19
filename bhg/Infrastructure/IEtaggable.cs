using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Infrastructure
{
    public interface IEtaggable
    {
        string GetEtag();
    }
}
