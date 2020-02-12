using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class IconsResponse : PagedCollection<Icon>
    {
        public Form IconsQuery { get; set; }
    }
}
