using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class AttachmentEntity
    {
        public Guid Id { get; set; }
        public Guid GemId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModDate { get; set; }

        [ForeignKey("GemId")]
        public GemEntity Gem { get; set; }
    }
}
