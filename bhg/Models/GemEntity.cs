using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class GemEntity
    {
        public GemEntity()
        {
            Attachments = new HashSet<AttachmentEntity>();
            PlusCodeLocals = new HashSet<PlusCodeLocalEntity>();
        }
        [Key]
        public Guid Id { get; set; }
        public Guid TreasureMapId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }
        public string MarkerIconUrl { get; set; }
        public string Website { get; set; }
        public DateTime CreateDate { get; set; }
        public string PlusCodeArea { get; set; }
        public DateTime? ModDate { get; set; }

        [ForeignKey("TreasureMapId")]
        public TreasureMapEntity TreasureMap { get; set; }

        public ICollection<PlusCodeLocalEntity> PlusCodeLocals { get; set; }
        public ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
