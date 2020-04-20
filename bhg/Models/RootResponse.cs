using System;
using bhg.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class RootResponse : Resource, IEtaggable
    {
        public Link Info { get; set; }

        public Link TreasureMaps { get; set; }

        public Link Users { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
