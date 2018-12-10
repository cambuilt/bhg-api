using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace bhg.Models
{
    public class Place
    {
        public Place()
        {
            Attachment = new HashSet<Attachment>();
        }
        public int PlaceId { get; set; }
        public int TreasureMapId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModDate { get; set; }

        public TreasureMap TreasureMap { get; set; }
        public ICollection<Attachment> Attachment { get; set; }
    }
}
