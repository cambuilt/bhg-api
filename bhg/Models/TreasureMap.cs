using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using bhg.Infrastructure;

namespace bhg.Models
{
    public class TreasureMap : Resource
    {
        public TreasureMap()
        {
            Gems = new HashSet<Gem>();
        }

        public Guid Id { get; set; }
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
        public string Author { get; set; }

        public double Latitude { get; set; }
        public double LatitudeDelta { get; set; }
        public double Longitude { get; set; }
        public double LongitudeDelta { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string Website { get; set; }
        public int Zoom { get; set; }
        public string Comments { get; set; }
        public ICollection<Gem> Gems { get; set; }

        public Form Gem { get; set; }
    }
}
