using AutoMapper;
using CelanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Entities;

namespace Valido.MobileBackend.Web.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
