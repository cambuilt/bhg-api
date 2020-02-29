using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class TreasureMapMappingProfile : Profile
    {
        public TreasureMapMappingProfile()
        {
            CreateMap<TreasureMap, TreasureMapModel>()
                .ReverseMap();
        }
    }
}
