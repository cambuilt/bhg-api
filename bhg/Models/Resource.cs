using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// comment

namespace bhg.Models
{
    public abstract class Resource : Link
    {
        [NotMapped]
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
