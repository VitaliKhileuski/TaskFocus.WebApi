using AutoMapper;
using TaskFocus.Data.Entities;
using TaskFocus.WebApi.Core.Models.User;

namespace TaskFocus.WebApi.Presentation.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserResponseModel>();
    }
}