using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class RouteLine : Resource
    {
        public Guid Id { get; set; }
        public Guid TreasureMapId { get; set; }
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double EndLatitude { get; set; }
        public double EndLongitude { get; set; }
        public int Sequence { get; set; }
    }
}
