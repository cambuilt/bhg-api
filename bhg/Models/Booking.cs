using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class Booking : Resource
    {
        public Link TreasureMap { get; set; }

        public Link User { get; set; } // TODO

        public DateTimeOffset StartAt { get; set; }

        public DateTimeOffset EndAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }

        public decimal Total { get; set; }
    }
}
