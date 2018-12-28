using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class OpeningEntity : BookingRange
    {
        public int TreasureMapId { get; set; }

        public int Rate { get; set; }
    }
}
