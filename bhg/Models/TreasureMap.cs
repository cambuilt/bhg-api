using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace bhg.Models
{
    public class TreasureMap
    {
        public TreasureMap()
        {
            Place = new HashSet<Place>();
        }

        public int TreasureMapId { get; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Area { get; set; }
        [Required]
        [StringLength(50)]
        public string Author { get; set; }
        public decimal Latitude { get; set; }
        public decimal LatitudeDelta { get; set; }
        public decimal Longitude { get; set; }
        public decimal LongitudeDelta { get; set; }
        public DateTime CreateDate { get; }
        public DateTime? ModDate { get; }

        public ICollection<Place> Place { get; set; }
    }
}
