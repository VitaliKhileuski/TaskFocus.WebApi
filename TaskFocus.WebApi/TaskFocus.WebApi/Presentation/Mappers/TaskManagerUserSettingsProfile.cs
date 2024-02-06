using AutoMapper;
using TaskFocus.Data.Entities;
using TaskFocus.WebApi.Core.Models.Settings;

namespace TaskFocus.WebApi.Presentation.Mappers;

public class TaskManagerUserSettingsProfile : Profile
{
    public TaskManagerUserSettingsProfile()
    {
        CreateMap<UserPriorityLevelModel, UserPriorityLevelEntity>().ReverseMap();
        CreateMap<UserTaskLabelModel, UserTaskLabelEntity>().ReverseMap();
        CreateMap<UpdateTaskManagerUserSettingsModel, TaskManagerUserSettingsEntity>();
        CreateMap<TaskManagerUserSettingsEntity, TaskManagerUserSettingsResponseModel>();
    }
}