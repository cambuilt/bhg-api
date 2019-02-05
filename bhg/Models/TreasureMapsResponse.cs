using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class TreasureMapsResponse : PagedCollection<TreasureMap>
    {
        public Form TreasureMapsQuery { get; set; }
    }
}
