using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class Attachment
    {
        public int AttachmentId { get; }
        public int PlaceId { get; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        public string Notes { get; set; }

        public Place Place { get; set; }
    }
}
