using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using bhg.Infrastructure;

namespace bhg.Models
{
    public class TreasureMapModel
    {
        [Sortable]
        [Searchable]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Sortable(Default = true)]
        [Searchable]
        [Required]
        [StringLength(50)]
        public string Area { get; set; }
        [Sortable]
        [Searchable]
        [Required]
        [StringLength(50)]
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Website { get; set; }
        public int Zoom { get; set; }
        public ICollection<Gem> Gems { get; set; }
        public ICollection<RouteLine> RouteLines { get; set; }
    }
}
