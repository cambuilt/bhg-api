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
        public int PlaceId { get; }
        public int TreasureMapId { get; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; }
        public DateTime? ModDate { get; }

        public TreasureMap TreasureMap { get; set; }
        public ICollection<Attachment> Attachment { get; set; }
    }
}
