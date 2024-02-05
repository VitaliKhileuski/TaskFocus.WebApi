using System.Linq;
using TaskFocus.Data.Entities;
using TaskFocus.Data.Interfaces;

namespace TaskFocus.Data.Repositories;

public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
{
    protected sealed override IQueryable<TaskEntity> CollectionWithIncludes { get; set; }
    
    public TaskRepository(Context context) : base(context)
    {
        CollectionWithIncludes = context.Tasks;
    }
}