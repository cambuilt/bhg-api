using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class TreasureMapsRespoonse : PagedCollection<TreasureMap>
    {
        public Link Openings { get; set; }
    }
}
