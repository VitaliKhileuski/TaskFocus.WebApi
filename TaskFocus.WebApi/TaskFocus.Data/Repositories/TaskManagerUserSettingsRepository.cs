using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskFocus.Data.Entities;
using TaskFocus.Data.Interfaces;

namespace TaskFocus.Data.Repositories;

public class TaskManagerUserSettingsRepository : BaseRepository<TaskManagerUserSettingsEntity>, ITaskManagerUserSettingsRepository
{
    protected sealed override IQueryable<TaskManagerUserSettingsEntity> CollectionWithIncludes { get; set; }
    
    public TaskManagerUserSettingsRepository(Context context) : base(context)
    {
        CollectionWithIncludes = context.TaskManagerUserSettings
            .Include(x => x.Labels)
            .Include(x => x.Priorities);
    }
}