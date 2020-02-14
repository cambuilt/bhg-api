using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class Icon : Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public ICollection<Gem> Gems { get; set; }
    }
}
