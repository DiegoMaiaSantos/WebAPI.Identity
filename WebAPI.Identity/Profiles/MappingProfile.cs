using AutoMapper;
using Domain;
using WebAPI.Identity.Dto;

namespace WebAPI.Identity.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}
