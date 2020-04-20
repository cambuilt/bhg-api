using System;
using System.Collections.Generic;

namespace bhg.Models
{
    public class TreasureMap : Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Author { get; set; }
        public double Latitude { get; set; }
        public double LatitudeDelta { get; set; }
        public double Longitude { get; set; }
        public double LongitudeDelta { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string Website { get; set; }
        public int Zoom { get; set; }
        public string Comments { get; set; }
        public string BannerImageUrl { get; set; }
        public ICollection<Gem> Gems { get; set; }
        public ICollection<RouteLine> RouteLines { get; set; }
        public Form Gem { get; set; }
    }
}
