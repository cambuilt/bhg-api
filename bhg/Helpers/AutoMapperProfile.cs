using AutoMapper;
using bhg.Dtos;
using bhg.Models;

namespace bhg.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}