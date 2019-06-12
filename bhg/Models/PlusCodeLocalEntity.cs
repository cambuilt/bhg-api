using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bhg.Models
{
    public class PlusCodeLocalEntity
    {
        public PlusCodeLocalEntity()
        {
        }
        [Key]
        public Guid Id { get; set; }
        public Guid GemId { get; set; }
        [StringLength(7)]
        public string LocalCode { get; set; }

        [ForeignKey("GemId")]
        public GemEntity Gem { get; set; }
    }
}
