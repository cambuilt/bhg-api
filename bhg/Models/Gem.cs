using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bhg.Models
{
    public class Gem : Resource
    {
        public Gem()
        {
            Attachments = new HashSet<Attachment>();
        }
        public Guid Id { get; set; }
        public Guid TreasureMapId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }
        public Guid IconId { get; set; }
        public string Website { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModDate { get; set; }

        [ForeignKey("TreasureMapId")]
        public TreasureMapEntity TreasureMap { get; set; }
        [ForeignKey("IconId")]
        public IconEntity Icon { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}
