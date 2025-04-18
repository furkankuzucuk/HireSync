
using AutoMapper;
using Project.Entities;
using Project.Entities.DataTransferObjects.User;

namespace Project.Services.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
    CreateMap<User,UserDto>().ReverseMap();
    CreateMap<UserDtoInsertion,User>().ReverseMap();
    CreateMap<UserDtoUpdate,User>().ReverseMap();
    }

}