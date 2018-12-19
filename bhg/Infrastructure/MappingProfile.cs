﻿using AutoMapper;
using bhg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bhg.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TreasureMapEntity, TreasureMap>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Capitalize(src.Name)))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.TreasureMapController.GetTreasureMapById), new { treasureMapId = src.TreasureMapId })));
        }

        private string Capitalize(string text)
        {
            StringBuilder sb = new StringBuilder(text.Length);
            bool capitalize = true;

            foreach (char c in text)
            {
                sb.Append(capitalize ? Char.ToUpper(c) : Char.ToLower(c));
                capitalize = !Char.IsLetter(c);
            }
            return sb.ToString();
        }
    }
}