using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace bhg.Models
{
    public class TreasureMap : Resource
    {
        public TreasureMap()
        {
            Place = new HashSet<Place>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Area { get; set; }
        [Required]
        [StringLength(50)]
        public string Author { get; set; }
        public double Latitude { get; set; }
        public double LatitudeDelta { get; set; }
        public double Longitude { get; set; }
        public double LongitudeDelta { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModDate { get; set; }

        public ICollection<Place> Place { get; set; }
    }
}
