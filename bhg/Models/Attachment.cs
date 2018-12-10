using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public int PlaceId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModDate { get; set; }

        public Place Place { get; set; }
    }
}
