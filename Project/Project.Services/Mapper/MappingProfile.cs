
using AutoMapper;
using Project.Entities;
using Project.Entities.DataTransferObjects.Job;
using Project.Entities.DataTransferObjects.Login;
using Project.Entities.DataTransferObjects.User;

namespace Project.Services.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
    CreateMap<User,UserDto>().ReverseMap();
    CreateMap<UserDtoInsertion,User>().ReverseMap();
    CreateMap<UserDtoUpdate,User>().ReverseMap();

    CreateMap<Login,LoginDto>().ReverseMap();
    CreateMap<LoginDtoInsertion,Login>().ReverseMap();
    CreateMap<LoginDtoUpdate,Login>().ReverseMap();

    CreateMap<Job, JobDto>().ReverseMap();
    CreateMap<JobInsertDto, Job>().ReverseMap();
    CreateMap<JobUpdateDto, Job>().ReverseMap();

    }

}