namespace TaskFocus.WebApi.Core.Models.Tasks;

public class UpdateTaskModel
{
    public Guid TaskId { get; set; }
    
    public Guid? LabelId { get; set; }
    
    public Guid? PriorityId { get; set; }
    
    public string Description { get; set; }
}