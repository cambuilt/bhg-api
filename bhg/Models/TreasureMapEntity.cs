using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bhg.Models
{
    public class TreasureMapEntity
    {
        public TreasureMapEntity()
        {
            Gems = new HashSet<GemEntity>();
        }

        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [ForeignKey("Area")]
        public virtual string Area { get; set; }
        public string Author { get; set; }
        public double Latitude { get; set; }
        public double LatitudeDelta { get; set; }
        public double Longitude { get; set; }
        public double LongitudeDelta { get; set; }
        public string Website { get; set; }
        public int Zoom { get; set; }
        public string Comments { get; set; }
        public string BannerImageUrl { get; set; }
        public ICollection<GemEntity> Gems { get; set; }
        public ICollection<RouteLineEntity> RouteLines { get; set; }
    }
}
