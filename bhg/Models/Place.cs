using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace bhg.Models
{
    public class Place
    {
        public int PlaceId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; }
        public DateTime? ModDate { get; }
    }
}
