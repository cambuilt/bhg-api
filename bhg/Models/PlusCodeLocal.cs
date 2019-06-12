using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bhg.Models
{
    public class PlusCodeLocal : Resource
    {
        public Guid Id { get; set; }
        public Guid GemId { get; set; }
        [StringLength(7)]
        public string LocalCode { get; set; }

        [ForeignKey("GemId")]
        public GemEntity Gem { get; set; }
    }
}
